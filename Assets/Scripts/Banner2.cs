using System;
using UnityEngine;
using GoogleMobileAds.Api;
public class Banner2 : MonoBehaviour
{
    public static Banner2 instance;
    private BannerView bannerView;
    public float tempoBanner = 5f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
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
            tempoBanner = 30f;
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

        string adUnitId = "ca-app-pub-3447901321096131/6998643990";


        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
}