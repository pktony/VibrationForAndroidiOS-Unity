using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace Sample
{
    public class UIs : MonoBehaviour
    {
        [SerializeField] private Button[] vibrateButtons = null;
        [SerializeField] private Button customizeButton = null;
        [SerializeField] private TMP_InputField patternInput = null;
        [SerializeField] private TMP_InputField amplitudeInput = null;
        [SerializeField] private Button githubButton = null;

        public Action defaultVibrationListener;
        public Action peekVibrationListener;
        public Action popVibrationListener;
        public Action nopeVibrationListener;
        public Action heavyVibrationListener;
        public Action mediumVibrationListener;
        public Action lightVibrationListener;
        public Action rigidVibrationListener;
        public Action softVibrationListener;
        public Action errorVibrationListener;
        public Action successVibrationListener;
        public Action warningVibrationListener;

        public Action customizedVibrationListener;
        public Action githubButtonListener;


        private void Awake()
        {
            vibrateButtons[0].onClick.AddListener(() => defaultVibrationListener?.Invoke());
            vibrateButtons[1].onClick.AddListener(() => peekVibrationListener?.Invoke());
            vibrateButtons[2].onClick.AddListener(() => popVibrationListener?.Invoke());
            vibrateButtons[3].onClick.AddListener(() => nopeVibrationListener?.Invoke());
            vibrateButtons[4].onClick.AddListener(() => heavyVibrationListener?.Invoke());
            vibrateButtons[5].onClick.AddListener(() => mediumVibrationListener?.Invoke());
            vibrateButtons[6].onClick.AddListener(() => lightVibrationListener?.Invoke());
            vibrateButtons[7].onClick.AddListener(() => rigidVibrationListener?.Invoke());
            vibrateButtons[8].onClick.AddListener(() => softVibrationListener?.Invoke());
            vibrateButtons[9].onClick.AddListener(() => errorVibrationListener?.Invoke());
            vibrateButtons[10].onClick.AddListener(() => successVibrationListener?.Invoke());
            vibrateButtons[11].onClick.AddListener(() => warningVibrationListener?.Invoke());

            customizeButton.onClick.AddListener(() => customizedVibrationListener?.Invoke());
            githubButton.onClick.AddListener(() => githubButtonListener?.Invoke());
        }

        public string GetPatternInput()
        {
            return patternInput.text;
        }

        public string GetAmplitudeInput()
        {
            return amplitudeInput.text;
        }
    }
}