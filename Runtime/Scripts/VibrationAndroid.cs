#if UNITY_ANDROID
using UnityEngine;

namespace VibrationUtility.Instance
{
    public class VibrationAndroid : VibrationInstance
    {
        private float intensity;

        public VibrationAndroid()
        {
            if(!IsVibrationAvailable())
                throw new System.NotSupportedException();
        }

        private AndroidJavaObject vibrator;
        public AndroidJavaObject Vibrator
        {
            get
            {
                if (vibrator == null)
                {
                    var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    var currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
                    vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                    Debug.Log($"Vibration Util : Android Vibrator {vibrator}");
                }

                return vibrator;
            }
        }

        private AndroidJavaClass vibrationEffectClass;
        public AndroidJavaClass VibrationEffectClass
        {
            get
            {
                vibrationEffectClass ??= new AndroidJavaClass("android.os.VibrationEffect");
                return vibrationEffectClass;
            }
        }

        private int androidApiLevel = 0;
        public int AndroidApiLevel
        {
            get
            {
                if (androidApiLevel <= 0)
                {
                    var buildVersionClass = new AndroidJavaClass("android.os.Build$VERSION");
                    androidApiLevel = buildVersionClass.GetStatic<int>("SDK_INT");
                }
                return androidApiLevel;
            }
        }

        public override void Vibrate(VibrationType vibrationType, float intensity)
        {
            if (!IsVibrationAvailable())
            {
                Debug.LogWarning("Vibration Util - Your device does not support Vibration");
                return;
            }

            this.intensity = intensity;

            switch (vibrationType)
            {
                case VibrationType.Peek:
                    CreateOneShot(5, 150);
                    break;
                case VibrationType.Pop:
                    CreateOneShot(10, 150);
                    break;
                case VibrationType.Nope:
                    var pattern = new long[4]
                    {
                        0, 10,
                        80, 5,
                    };
                    var patternAmplitude = new int[4]
                    {
                        0, 200,
                        0, 200,
                    };
                    CreateWaveform(pattern, patternAmplitude, -1);
                    break;
                case VibrationType.Heavy:
                    CreateOneShot(10, 150);
                    break;
                case VibrationType.Medium:
                    CreateOneShot(10, 100);
                    break;
                case VibrationType.Light:
                    CreateOneShot(10, 50);
                    break;
                case VibrationType.Rigid:
                    CreateOneShot(5, 100);
                    break;
                case VibrationType.Soft:
                    CreateOneShot(15, 50);
                    break;
                case VibrationType.Error:
                    pattern = new long[8]
                    {
                        0, 10,
                        100, 10,
                        100, 10,
                        100, 15
                    };
                    patternAmplitude = new int[8]
                    {
                        0, 100,
                        0, 100,
                        0, 150,
                        0, 100
                    };
                    CreateWaveform(pattern, patternAmplitude, -1);
                    break;
                case VibrationType.Success:
                    pattern = new long[4]
                    {
                        0, 10,
                        200, 10
                    };
                    patternAmplitude = new int[4]
                    {
                        0, 150,
                        0, 200
                    };
                    CreateWaveform(pattern, patternAmplitude, -1);
                    break;
                case VibrationType.Warning:
                    pattern = new long[4]
                    {
                        0, 10,
                        250, 10
                    };
                    patternAmplitude = new int[4]
                    {
                        0, 150,
                        0, 100
                    };
                    CreateWaveform(pattern, patternAmplitude, -1);
                    break;
                default:
                    CreateOneShot(500, -1);
                    break;
            }

            /// Pattern : { 1000, 200, 1000, 200}
            /// 1초 쉬고, 0.2초 진동, 1초 쉬고, 0.2초 진동
            /// Repeat : 1 = true , -1 : false
            //Vibrator.Call("vibrate", pattern , -1); //deprecated in API 26
        }

        /// <summary>
        /// Customizable vibration <br/>
        /// Android Only : >= API 26
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="amplitude"></param>
        public override void VibrateCustom(long[] pattern, int[] amplitude)
        {
            if (pattern == null || amplitude == null
                || pattern.Length == 0 || amplitude.Length == 0)
            {
                    Debug.LogWarning("Vibration Util : Check your customized input!");
                return;
            }

            CreateWaveform(pattern, amplitude, -1);
        }

        /// <summary>
        /// Vibrates for certain duration. <br/>
        /// Android Only !!!
        /// </summary>
        /// <param name="duration">milli second</param>
        /// <param name="amplitude">1 ~ 255</param>
        public void VibrateFor(long duration, int amplitude)
        {
			Debug.LogWarning("Vibration Util : Android only funcion -- VibrateFor()");

            CreateOneShot(duration, amplitude);
        }

        private void CreateOneShot(long milliseconds, int amplitude)
        {
            var vibrateEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", new object[] { milliseconds, amplitude });
            Vibrator.Call("vibrate", vibrateEffect);
        }

        private void CreateWaveform(long[] pattern, int[] amplitude, int repeat)
        {
            var vibrateEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", new object[] { pattern, amplitude, repeat });
            Vibrator.Call("vibrate", vibrateEffect);
        }

        public override bool IsVibrationAvailable()
        {
            var hasVibrator = Vibrator.Call<bool>("hasVibrator");
            return  hasVibrator && AndroidApiLevel >= 26;
        }
    }
}
#endif