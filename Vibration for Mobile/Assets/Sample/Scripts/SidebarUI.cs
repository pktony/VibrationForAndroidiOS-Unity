using System;

using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public enum SidebarAnimationType
    {
        open = 0, close = 1
    }

    public class SidebarUI : MonoBehaviour
    {
        [SerializeField] private Animation anim;

        [SerializeField] private AnimationClip[] clips;        
        [SerializeField] private Button aosVibratorLinkButton;
        [SerializeField] private Button aosVibratorEffectLinkButton;
        [SerializeField] private Button iosfeedbackLinkButton;
        [SerializeField] private Button iosImpactFeedbackLinkButton;
        [SerializeField] private Button iosNotifiactionFeedbackLinkButton;

        [SerializeField] private Button closeButton;
        [SerializeField] private Button feedbackButton;

        private void Awake()
        {
            aosVibratorLinkButton.onClick.AddListener(OpenAOSVibratorLink);
            aosVibratorEffectLinkButton.onClick.AddListener(OpenAOSVibrationEffectLink);
            iosfeedbackLinkButton.onClick.AddListener(OpeniOSFeedbackLink);
            iosImpactFeedbackLinkButton.onClick.AddListener(OpeniOSImpactFeedbackLink);
            iosNotifiactionFeedbackLinkButton.onClick.AddListener(OpeniOSNotifiactionFeedbackLink);
            closeButton.onClick.AddListener(CloseSidebar);
            feedbackButton.onClick.AddListener(OpenFeedbackLink);
        }

        public void OpenSidebar()
        {
            Keyframe[] keyframes = new Keyframe[2];
            keyframes[0] = new Keyframe(0f, -Camera.main.pixelWidth);
            keyframes[1] = new Keyframe(0.3f, 0f);
            AnimationCurve curve = new(keyframes);

            clips[(int)SidebarAnimationType.open].SetCurve(
                "Group", typeof(RectTransform),
                "m_AnchoredPosition.x", curve);
            anim.Play(clips[(int)SidebarAnimationType.open].name);
        }

        public void CloseSidebar()
        {
            Keyframe[] keyframes = new Keyframe[2];
            keyframes[0] = new Keyframe(0f, 0f);
            keyframes[1] = new Keyframe(0.3f, -Camera.main.pixelWidth);
            AnimationCurve curve = new(keyframes);
            
            clips[(int)SidebarAnimationType.close].SetCurve(
                "Group", typeof(RectTransform),
                "m_AnchoredPosition.x", curve);

            anim.Play(clips[(int)SidebarAnimationType.close].name);
        }

        private void OpenAOSVibratorLink()
        {
            Application.OpenURL("https://developer.android.com/reference/android/os/Vibrator");
        }

        private void OpenAOSVibrationEffectLink()
        {
            Application.OpenURL("https://developer.android.com/reference/android/os/VibrationEffect");
        }

        private void OpeniOSImpactFeedbackLink()
        {
            Application.OpenURL("https://developer.apple.com/documentation/uikit/uiimpactfeedbackgenerator");
        }

        private void OpeniOSNotifiactionFeedbackLink()
        {
            Application.OpenURL("https://developer.apple.com/documentation/uikit/uinotificationfeedbackgenerator");
        }

        private void OpeniOSFeedbackLink()
        {
            Application.OpenURL("https://developer.apple.com/documentation/uikit/uifeedbackgenerator");
        }

        private void OpenFeedbackLink()
        {
            Application.OpenURL("https://github.com/pktony/VibrationForAndroidiOS-Unity/issues");
        }
    }
}