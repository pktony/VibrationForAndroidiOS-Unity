using UnityEngine;

namespace VibrationUtility
{
    public class VibrationUtil
    {
#if UNITY_IOS
		[DllImport("__Internal")]
		public static extern void Vibrate(int _n);
		[DllImport("__Internal")]
		public static extern void _impactOccurred(string style);
		[DllImport("__Internal")]
		public static extern void _notificationOccurred(string style);
#endif

#if UNITY_ANDROID
		private static AndroidJavaObject vibrator;
		public static AndroidJavaObject Vibrator
		{
			get
			{
				if(vibrator == null)
				{
					var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
					var currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");
					vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
				}

				return vibrator;
			}
		}

		private static AndroidJavaClass vibrationEffectClass;
		public static AndroidJavaClass VibrationEffectClass
		{
			get
			{
				if (vibrationEffectClass == null)
				{
					vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
					//defaultAmplitude = vibrationEffectClass.GetStatic<int>("DEFAULT_AMPLITUDE");
					//Debug.Log($"Vibration Default Amplitude : {defaultAmplitude}");
				}
				return vibrationEffectClass;
			}
		}
#endif

        /// <summary>
        /// Vibrate both for Android / iOS <br/>
		/// Android API >= 26
        /// </summary>
        /// <param name="vibrationType"></param>
        /// <param name="duration"> milli second</param>
        public static void Vibrate(VibrationType vibrationType)
        {
            if (!Application.isMobilePlatform) return;
#if UNITY_ANDROID
			VibrateAndroid(vibrationType);
#elif UNITY_IOS
			switch(vibrationType)
			{
				case VibrationType.Default:
				case VibrationType.Peek:
				case VibrationType.Pop:
				case VibrationType.Nope:
					Vibrate((int)vibrationType);
					break;

				case VibrationType.Heavy:
				case VibrationType.Medium:
				case VibrationType.Light:
				case VibrationType.Rigid:
				case VibrationType.Soft:
					_impactOccurred(vibrationType.ToString());
					break;
				default:
					_notificationOccurred(vibrationType.ToString());
					break;
			}
#endif
        }


#if UNITY_ANDROID
        /// <summary>
        /// Customizable vibration <br/>
		/// Android Only : >= API 26
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="amplitude"></param>
        public static void VibrateCustom(long[] pattern, int[] amplitude)
        {
            if (pattern == null || amplitude == null)
            {
                if (pattern == null)
                    Debug.LogWarning("Pattern is null");
                if (amplitude == null)
                    Debug.LogWarning("Amplitude is null");

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
		public static void VibrateFor(long duration, int amplitude)
		{
#if !UNITY_ANDROID
			Debug.LogWarning("Vibration Util -- Android only funcion -- VibrateFor()");
			return;
#endif
			CreateOneShot(duration, amplitude);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vibrationType">Type of Vibration</param>
        /// <param name="amplitude">must be between 1~255 :::
        /// System Default Value is -1 </param>
        private static void VibrateAndroid(VibrationType vibrationType)
		{
			switch (vibrationType)
			{
				case VibrationType.Peek:
					break;
				case VibrationType.Pop:
					CreateOneShot(LengthType.VeryLong.time, -1);
					break;
				case VibrationType.Nope:
					break;
				case VibrationType.Heavy:
					CreateOneShot(LengthType.Medium.time, AmplitudeType.heavy.amplitude);
                    break;
				case VibrationType.Medium:
                    CreateOneShot(LengthType.Medium.time, AmplitudeType.medium.amplitude);
                    break;
				case VibrationType.Light:
                    CreateOneShot(LengthType.Medium.time, AmplitudeType.light.amplitude);
                    break;
				case VibrationType.Rigid:
                    CreateOneShot(LengthType.Short.time, AmplitudeType.rigid.amplitude);
                    break;
                case VibrationType.Soft:
                    CreateOneShot(LengthType.Long.time, AmplitudeType.light.amplitude);
                    break;
                case VibrationType.Error:
					var pattern = new long[8]
					{
						0, 50,
						30, 50,
						30, 50,
						30, 80
					};
					var patternAmplitude = new int[8]
					{
						0, AmplitudeType.medium.amplitude,
						0, AmplitudeType.medium.amplitude,
						0, AmplitudeType.medium.amplitude,
						0, AmplitudeType.medium.amplitude
					};
					CreateWaveform(pattern, patternAmplitude, -1);
					break;
				case VibrationType.Success:
                    pattern = new long[4]
					{
						0, 30,
						30, 30
					};
                    patternAmplitude = new int[4]
					{
						0, AmplitudeType.medium.amplitude,
						0, AmplitudeType.heavy.amplitude
					};
                    CreateWaveform(pattern, patternAmplitude, -1);
                    break;
				case VibrationType.Warning:
                    pattern = new long[4]
					{
						0, 30,
						80, 30
					};
                    patternAmplitude = new int[4]
					{
						0, AmplitudeType.heavy.amplitude,
						0, AmplitudeType.medium.amplitude
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

		private static void CreateOneShot(long milliseconds, int amplitude)
		{
			var vibrateEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", new object[] { milliseconds, amplitude });
			Vibrator.Call("vibrate", vibrateEffect);
		}

		private static void CreateWaveform(long[] pattern, int[] amplitude, int repeat)
		{
			var vibrateEffect = VibrationEffectClass.CallStatic<AndroidJavaObject>("createWaveform", new object[] { pattern, amplitude, repeat });
			Vibrator.Call("vibrate", vibrateEffect);
		}
#endif
    }

	/// <summary>
	/// These vibration types are defined according to iOS vibration types.</br>
	/// See official document links listed below for details. </br>
	/// Android vibrations are customized.
	/// </summary>
    public enum VibrationType
    {
        //System Sound Type
        //https://developer.apple.com/documentation/audiotoolbox/1405202-audioservicesplayalertsound
        //https://github.com/TUNER88/iOSSystemSoundsLibrary
        //https://nikaeblog.wordpress.com/2017/07/11/system-sound-id-list-ios/
        Default = 1352, // (iOS) Vibrates regardless of system sound setting
        Peek = 1519,
        Pop = 1520,
        Nope = 1521,

        //Impact Type
        //https://developer.apple.com/documentation/uikit/uiimpactfeedbackstyle?language=objc
        Heavy,
        Medium,
        Light,
        Rigid,
        Soft,

        //Notification Type
        //https://developer.apple.com/documentation/uikit/uinotificationfeedbackgenerator/feedbacktype
        Error,
        Success,
        Warning,
    }

    /// <summary>
    /// (Customized) Vibration Amplitude Types <br/>
    /// These values are adjusted by trial and error on Samsung Galaxy.<br/>
    /// Change <see cref="defaultAmplitude"/> if neccessary.<br/>
    /// </summary>
    public class AmplitudeType
	{
		public int amplitude;
		public AmplitudeType(int amplitude)
		{
			this.amplitude = amplitude;
		}

        /// <summary>
        /// Must be between 1 ~ 255
        /// </summary>
        private const int defaultAmplitude = 100;

		public static AmplitudeType medium = new(defaultAmplitude);
		public static AmplitudeType heavy = new((int)(defaultAmplitude * 1.5f));
		public static AmplitudeType light = new((int)(defaultAmplitude * 0.8f));
		public static AmplitudeType rigid = new((int)(defaultAmplitude * 1.1f));
    }

    /// <summary>
    /// (Customized) Vibration length (duration) types <br/>
    /// These values are adjusted by trial and error on Samsung Galaxy.<br/>
    /// Change <see cref="defaultLength"/> if neccessary.<br/>
    /// </summary>
    public class LengthType
	{
		public long time;

		public LengthType(long time)
		{
			this.time = time;
		}

		private const long defaultLength = 30;

		public static LengthType Medium = new(defaultLength);
		public static LengthType Short = new(defaultLength - 20);
		public static LengthType Long = new(defaultLength + 20);
		public static LengthType VeryLong = new(defaultLength + 50);
    }
}