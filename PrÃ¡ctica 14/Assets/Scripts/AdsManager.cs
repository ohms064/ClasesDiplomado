using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
    public static AdsManager instance;
	// Use this for initialization
	void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	public void ShowAd () {
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        }
	}
}
