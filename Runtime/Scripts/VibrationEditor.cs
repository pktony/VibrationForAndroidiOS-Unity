using System;

namespace VibrationUtility.Instance
{
    public class VibrationEditor : VibrationInstance
    {
        public override bool IsVibrationAvailable()
        {
            return false;
        }

        public override void Vibrate(VibrationType _, float __)
        {
            throw new NotSupportedException();
        }

        public override void VibrateCustom(long[] _, int[] __)
        {
            throw new NotSupportedException();
        }
    }
}