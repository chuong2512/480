/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System;

public class AdsControl : MonoBehaviour
{


	protected AdsControl ()
	{
	}

	private static AdsControl _instance;
	public string AdmobID_Android, AdmobID_IOS;
//	public string RewardVideo_IOS, RewardVideo_Android;


	public static AdsControl Instance { get { return _instance; } }

	void Awake ()
	{
		if (FindObjectsOfType (typeof(AdsControl)).Length > 1) {
			Destroy (gameObject);
			return;
		}

		_instance = this;
		MakeNewInterstial ();
//		RequestRewardBasedVideo ();

		DontDestroyOnLoad (gameObject); //Already done by CBManager




	}


	public void HandleInterstialAdClosed (object sender, EventArgs args)
	{




	}

	void MakeNewInterstial ()
	{




	}


	public void showAds ()
	{
		Debug.Log ("Show ads");



	}


	public bool GetRewardAvailable ()
	{
		bool avaiable = false;

		return avaiable;
	}

//	public void ShowRewardVideo ()
//	{
//
//		if (rewardBasedVideo.IsLoaded ()) {
//			rewardBasedVideo.Show ();
//		}
//
//	}

	public void HideBannerAds ()
	{
	}

	public void ShowBannerAds ()
	{
	}

//	private void RequestRewardBasedVideo ()
//	{
//		
//		#if UNITY_ANDROID
//		string adUnitId = RewardVideo_Android;
//		#elif UNITY_IPHONE
//		string adUnitId = RewardVideo_IOS;
//		#else
//		string adUnitId = "unexpected_platform";
//		#endif
//		rewardBasedVideo = RewardBasedVideoAd.Instance;
//
//		AdRequest request = new AdRequest.Builder ().Build ();
//		rewardBasedVideo.LoadAd (request, adUnitId);
//		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
//		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
//	}

//	public void HandleRewardBasedVideoRewarded (object sender, Reward args)
//	{
//		string type = args.Type;
//		double amount = args.Amount;
//		print ("User rewarded with: " + amount.ToString () + " " + type);
//		FindObjectOfType<Menu> ().Purchase (3);
//	}
//
//	public void HandleRewardBasedVideoClosed (object sender, EventArgs args)
//	{
//		RequestRewardBasedVideo ();
//	}
	/*
	private void HandleShowResult (ShowResult result)
	{
		switch (result) {
		case ShowResult.Finished:
			FindObjectOfType<Menu>().Purchase(3);
			break;
		case ShowResult.Skipped:
			break;
		case ShowResult.Failed:
			break;
		}
	}
*/


}

