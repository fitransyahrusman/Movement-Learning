using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;


public class AdMob : MonoBehaviour
{
    public BannerAdGameObject banner;

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize((success) => { });
        banner = MobileAds.Instance.GetAd<BannerAdGameObject>("Banner Ad");
        banner.LoadAd();
    }
}
