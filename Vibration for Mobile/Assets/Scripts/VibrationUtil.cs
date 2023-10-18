using System;
using UnityEngine;

namespace VibrationUtility
{
    using Instance;

    public class VibrationUtil
    { 
		private static VibrationInstance vibrationInstance;

		public static void Init()
		{
            try
            {
#if UNITY_EDITOR
                vibrationInstance = new VibrationEditor();
#elif UNITY_ANDROID
                vibrationInstance = new VibrationAndroid();
#elif UNITY_IOS
                vibrationInstance = new VibrationIOS();
#endif
            }
            catch (NotSupportedException e)
            {
                Debug.LogError($"Vibration Utility - {e}");
                return;
            }
            catch (Exception e)
            {
                Debug.LogError($"Vibration Utility - Failed to Initialize : {e}");
                return;
            }
            Debug.Log($"Vibration Utility - Initialized Successfully {vibrationInstance}");
        }

        /// <summary>
        /// Vibrate using pre-defined types.<br/>
        /// note : Customized vibration is allowed only in Android
        /// </summary>
        /// <param name="vibrationType">Pre-defined Vibration Types<br/>
        /// see <see cref="VibrationType"/>
        /// </param>
        public static void Vibrate(VibrationType vibrationType, float intensity = 1.0f)
        {
            if (!Application.isMobilePlatform) return;
            if (vibrationInstance == null)
            {
                Debug.LogWarning($"Vibration Util : Vibrator is null. Initialize First");
                return;
            }

            vibrationInstance.Vibrate(vibrationType, intensity);
		}

        public static void VibrateCustomized(long[] pattern, int[] amplitude)
        {
#if !UNITY_ANDROID
            return;
#endif

            vibrationInstance.VibrateCustom(pattern, amplitude);
        }

        public static bool IsVibrationAvailable()
        {
            return vibrationInstance.IsVibrationAvailable();
        }
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
        //https://iphonedev.wiki/index.php/AudioServices
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

        public static AmplitudeType veryWeak = new(10);
        public static AmplitudeType weak = new(20);
        public static AmplitudeType strong = new(150);
        public static AmplitudeType veryStrong = new(200);

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