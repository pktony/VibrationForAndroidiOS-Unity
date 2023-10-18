using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Sample
{
    public class Admanager : MonoBehaviour
    {
        private static Admanager instance;
        public static Admanager Inst
        {
            get
            {
                if (instance == null)
                {
                    var obj = FindObjectOfType<Admanager>();
                    if (obj != null)
                    {
                        instance = obj;
                    }
                    else
                    {
                        GameObject gameObject = new();
                        gameObject.name = $"{typeof(Admanager).Name}";
                        instance = gameObject.AddComponent<Admanager>();
                    }
                }

                DontDestroyOnLoad(instance.gameObject);
                return instance;
            }
        }

        //bool isInitialized = false;
        private BannerView banner;
        private InterstitialAd interAd;
        [SerializeField] private string bannerID = "ca-app-pub-3940256099942544/6300978111";
        [SerializeField] private string intersitialID = "ca-app-pub-3940256099942544/1033173712";

        public void Init()
        {
            MobileAds.Initialize((initStatus) =>
            {
                //isInitialized = true;

                var text = Resources.Load<TextAsset>("admobID");
                var ids = JsonUtility.FromJson<AdmobID>(text.ToString());

                bannerID = ids.bannerAd;
                intersitialID = ids.interAd;

                //Debug.Log($"Banner Ad ID : {bannerID}");
                //Debug.Log($"inter Ad ID : {intersitialID}");

                //ShowInterstitialAd();
                ShowBannerAd();
            });
        }

        private void CreateBanner()
        {
            if(banner != null)
            {
                banner.Destroy();
                banner = null;
            }

            var size = AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(300);
            banner = new BannerView(bannerID, size, AdPosition.Bottom);
        }


        public void ShowBannerAd()
        {
            if(banner == null)
            {
                CreateBanner();
            }

            var adRequest = new AdRequest();
            banner.LoadAd(adRequest);
        }

        public void SetActiveBannerAd(bool isActive)
        {
            if (banner == null)
            {
                ShowBannerAd();
            }

            if (isActive)
                banner.Show();
            else
                banner.Hide();
        }

        private void LoadInterstitialAd(Action loadCompleteAction)
        {
            if (interAd != null)
            {
                interAd.Destroy();
                interAd = null;
            }

            var request = new AdRequest();

            InterstitialAd.Load(intersitialID, request,
                (InterstitialAd ad, LoadAdError err) =>
                {
                    if(err != null || ad == null)
                    {
                        Debug.LogError("Error Loading Interstitial Ad");
                        return;
                    }

                    interAd = ad;

                    loadCompleteAction?.Invoke();
                });
        }

        public void ShowInterstitialAd()
        {
            if (interAd == null)
            {
                LoadInterstitialAd(() => interAd.Show()) ;
            }

            if(interAd.CanShowAd())
                interAd.Show();
        }
    }

    [Serializable]
    public class AdmobID
    {
        public string bannerAd;
        public string interAd;
    }
}