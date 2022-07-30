using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ADC_SUdoku : MonoBehaviour
{
    public string AppId;
    public string BannerADid;
    public string InterstitialADid;


    public AdPosition BannerPosition;
    public bool TestDevice = false;

    public static ADC_SUdoku _instence;

    private BannerView _baneView;
    private InterstitialAd _interstitial;

    public void Awake()
    {
        if (_instence == null)
        {
            _instence = this;
            DontDestroyOnLoad(_instence);
        }
        else
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(AppId);

        this.CreateBannerAD(CreateRequest());
        this.CreateInterstitialAd(CreateRequest());
    }


    private AdRequest CreateRequest()
    {
        AdRequest request;
        if (TestDevice)
        {
            request = new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build();
        } else
        {
            request = new AdRequest.Builder().Build();
        }

        return request;
    }


    #region InterstitialAd

    public void CreateInterstitialAd(AdRequest request)
    {
        this._interstitial = new InterstitialAd(InterstitialADid);
        this._interstitial.LoadAd(request);
    }

    public void ShowInterstitialAD()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }
        this._interstitial.LoadAd(CreateRequest());
    }
    #endregion


    #region BannerAD
    public void CreateBannerAD(AdRequest request)
    {
        this._baneView = new BannerView(BannerADid,AdSize.SmartBanner,BannerPosition);
        this._baneView.LoadAd(request);

        HideBanner();
    }
    
    public void HideBanner()
    {
        _baneView.Hide();
    }

    public void ShowBanner()
    {
        _baneView.Show();
    }
    #endregion
}
