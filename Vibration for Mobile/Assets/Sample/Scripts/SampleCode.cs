using System;
using System.Collections.Generic;
using UnityEngine;

using VibrationUtility;

namespace Sample
{
    public class SampleCode : MonoBehaviour
    {
        [SerializeField] private UIController uis = null;

        private void Awake()
        {
            Application.targetFrameRate = 61;
            SetListeners();
            VibrationUtil.Init();
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
            var patternInput = uis.GetPatternInput().Split(',');
            var amplitudeInput = uis.GetAmplitudeInput().Split(',');

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
            VibrationUtil.Vibrate(VibrationType.Warning);
        }

        private void VibrateSuccess()
        {
            VibrationUtil.Vibrate(VibrationType.Success);
        }

        private void VibrateError()
        {
            VibrationUtil.Vibrate(VibrationType.Error);
        }

        private void VibrateSoft()
        {
            VibrationUtil.Vibrate(VibrationType.Soft);
        }

        private void VibrateRigid()
        {
            VibrationUtil.Vibrate(VibrationType.Rigid);
        }

        private void VibrateLight()
        {
            VibrationUtil.Vibrate(VibrationType.Light);
        }

        private void VibrateMedium()
        {
            VibrationUtil.Vibrate(VibrationType.Medium);
        }

        private void VibrateHeavy()
        {
            VibrationUtil.Vibrate(VibrationType.Heavy);
        }

        private void VibrateNope()
        {
            VibrationUtil.Vibrate(VibrationType.Nope);
        }

        private void VibratePop()
        {
            VibrationUtil.Vibrate(VibrationType.Pop);
        }

        private void VibratePeek()
        {
            VibrationUtil.Vibrate(VibrationType.Peek);
        }

        private void VibrateDefault()
        {
            VibrationUtil.Vibrate(VibrationType.Default);
        }

        private void OpenGithubPage()
        {
            Application.OpenURL("https://github.com/pktony/VibrationForAndroidiOS-Unity");
        }
    }
}