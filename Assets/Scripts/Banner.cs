using System;
using UnityEngine;
using GoogleMobileAds.Api;
public class Banner : MonoBehaviour
{
    public static Banner instance;
    private BannerView bannerView;
    public float tempoBanner = 15f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        string appId = "ca-app-pub-3447901321096131~7608836904";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }

    void Update()
    {
        tempoBanner = tempoBanner - Time.deltaTime;
        if (tempoBanner <= 0)
        {
            this.RequestBanner();
            tempoBanner = 45f;
        }
    }

    private void DeletBannerViwer()
    {
        bannerView.Destroy();
    }

    private void RequestBanner()
    {
        if (GameObject.Find("BANNER(Clone)"))
        {
            Destroy(GameObject.Find("BANNER(Clone)"));
        }

        string adUnitId = "ca-app-pub-3447901321096131/7061207849";

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.BottomLeft);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}