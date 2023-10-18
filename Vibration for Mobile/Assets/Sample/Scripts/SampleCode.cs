using System;
using System.Collections.Generic;
using UnityEngine;

using VibrationUtility;

namespace Sample
{
    public class SampleCode : MonoBehaviour
    {
        [SerializeField] private UIController uis = null;
        [SerializeField] private NativeUtility nativeUtility = null; 

        private void Awake()
        {
            Application.targetFrameRate = 61;
            SetListeners();
            try
            {
                VibrationUtil.Init();
            }catch(NotSupportedException)
            {
                nativeUtility.ShowToastPopup("Unsupported Device : No available vibrator");
            }

            Admanager.Inst.Init();
        }

        private void SetListeners()
        {
            uis.defaultVibrationListener = VibrateDefault;
            uis.peekVibrationListener = VibratePeek;
            uis.popVibrationListener = VibratePop;
            uis.nopeVibrationListener = VibrateNope;
            uis.heavyVibrationListener = VibrateHeavy;
            uis.mediumVibrationListener = VibrateMedium;
            uis.lightVibrationListener = VibrateLight;
            uis.rigidVibrationListener = VibrateRigid;
            uis.softVibrationListener = VibrateSoft;
            uis.errorVibrationListener = VibrateError;
            uis.successVibrationListener = VibrateSuccess;
            uis.warningVibrationListener = VibrateWarning;

            uis.customizedVibrationListener = VibrateCustomized;
            uis.githubButtonListener = OpenGithubPage;
        }

        private void VibrateCustomized()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");

            var patternText = uis.GetPatternInput();
            var amplitudeText = uis.GetAmplitudeInput();

            if (string.IsNullOrEmpty(patternText) || string.IsNullOrEmpty(amplitudeText))
            {
                nativeUtility.ShowToastPopup("Fill in customize input !");
                return;
            }

            var patternInput = patternText.Split(',');
            var amplitudeInput = amplitudeText.Split(',');

            if(patternInput.Length != amplitudeInput.Length)
            {
                nativeUtility.ShowToastPopup("Amplitude and Pattern must have same length");
                return;
            }

            List<long> pattern = new();
            foreach(var num in patternInput)
            {
                if(!long.TryParse(num, out var result))
                {
                    Debug.Log("Invalid Character !");
                    return;
                }

                pattern.Add(result);
            }

            List<int> amplitude = new();
            foreach (var num in amplitudeInput)
            {
                if (!int.TryParse(num, out var result))
                {
                    Debug.Log("Invalid Character !");
                    return;
                }

                amplitude.Add(result);
            }

            VibrationUtil.VibrateCustomized(pattern.ToArray(), amplitude.ToArray());
        }

        private void VibrateWarning()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Warning);
        }

        private void VibrateSuccess()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Success);
        }

        private void VibrateError()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Error);
        }

        private void VibrateSoft()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Soft);
        }

        private void VibrateRigid()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Rigid);
        }

        private void VibrateLight()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Light);
        }

        private void VibrateMedium()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Medium);
        }

        private void VibrateHeavy()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Heavy);
        }

        private void VibrateNope()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Nope);
        }

        private void VibratePop()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Pop);
        }

        private void VibratePeek()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Peek);
        }

        private void VibrateDefault()
        {
            if (!VibrationUtil.IsVibrationAvailable())
                nativeUtility.ShowToastPopup("Unsupported Device : No Vibrator");
            VibrationUtil.Vibrate(VibrationType.Default);
        }

        private void OpenGithubPage()
        {
            Application.OpenURL("https://github.com/pktony/VibrationForAndroidiOS-Unity");
        }
    }
}