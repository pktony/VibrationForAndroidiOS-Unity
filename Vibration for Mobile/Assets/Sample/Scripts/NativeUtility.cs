using UnityEngine;

namespace Sample
{
    public class NativeUtility : MonoBehaviour
    {
#if UNITY_ANDROID
        private AndroidJavaClass unityPlayer;
        private AndroidJavaObject unityActivity;
        private AndroidJavaClass toastClass;

        public AndroidJavaClass UnityPlayer
        {
            get
            {
                if(unityPlayer == null)
                {
                    unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    Debug.Log($"Unity Player : {unityPlayer}");
                }

                return unityPlayer;
            }
        }

        public AndroidJavaObject CurrentActivity 
        {
            get
            {
                if (unityActivity == null)
                {
                    unityActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    Debug.Log($"Unity Player : {unityActivity}");
                }

                return unityActivity;
            }
        }

        public AndroidJavaClass ToastClass
        {
            get
            {
                if (toastClass == null)
                {
                    toastClass = new AndroidJavaClass("android.widget.Toast");
                    Debug.Log($"Toast Class : {toastClass}");
                }

                return toastClass;
            }
        }
#endif

        public void ShowToastPopup(string message)
        {
            if (Application.isEditor) return;
#if UNITY_ANDROID
            CurrentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = ToastClass.CallStatic<AndroidJavaObject>("makeText", CurrentActivity, message, 2);
                Debug.Log($"Toast object : {toastObject}");
                toastObject.Call("show");
            }));
#endif
        }
    }
}