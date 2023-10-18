namespace VibrationUtility.Instance
{
    public abstract class VibrationInstance
    {
        /// <summary>
        /// Vibrate using pre-defined types.<br/>
        /// note : Customized vibration is allowed only in Android
        /// </summary>
        /// <param name="vibrationType">Pre-defined Vibration Types<br/>
        /// see <see cref="VibrationType"/>
        /// </param>
        public abstract void Vibrate(VibrationType vibrationType, float intensity);
        public abstract void VibrateCustom(long[] pattern, int[] amplitude);

        public abstract bool IsVibrationAvailable();
    }
}