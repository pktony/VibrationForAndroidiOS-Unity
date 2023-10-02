#if UNITY_IOS
using System.Runtime.InteropServices;

namespace VibrationUtility.Instance
{
    public class VibrationIOS : VibrationInstance
    {

		[DllImport("__Internal")]
		public static extern void Vibrate(int _n);
		[DllImport("__Internal")]
		public static extern void _impactOccurred(string style);
		[DllImport("__Internal")]
		public static extern void _notificationOccurred(string style);
		
        public override void Vibrate(VibrationType vibrationType)
        {
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
        }

        public override void VibrateCustom(long[] pattern, int[] amplitude)
        {
            throw new System.NotImplementedException();
        }

        protected override bool IsVibrationAvailable()
        {
            return true;
		}
	}
}
#endif
