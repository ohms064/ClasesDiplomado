using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public GameObject facebookComposerPanel;
	public GameObject shareButton;
	public Image screenshotPreview;
    public InputField message2Share;

	void Start () 
	{
		facebookComposerPanel.SetActive (false);
		shareButton.SetActive (true);

    }

	public void ShareToFacebook ()
	{
		//facebookComposerPanel.SetActive (true);
		shareButton.SetActive (false);
		Utilities.instance.captureScreenShotCallback += ScreenShotCallback;
		Utilities.instance.TakeScreenShot ();
        AnalyticsManager.instance.ScreenEvent("FacebookShare Screen");
		Time.timeScale = 0f;
	}

	public void CancelShare()
	{
		facebookComposerPanel.SetActive (false);
		shareButton.SetActive (true);
		Time.timeScale = 1f;
	}

	public void ConfirmShareToFacebook()
	{
		facebookComposerPanel.SetActive (false);
        FacebookManager.instance.ShareMessage(message2Share.text, Utilities.instance.screenShot);
		shareButton.SetActive (true);
		Time.timeScale = 1f;
        AnalyticsManager.instance.RegisterEvent("Social", "Facebook Share", "Facebook", (long)Time.time);
	}

	public void ScreenShotCallback()
	{
		screenshotPreview.sprite = Sprite.Create (Utilities.instance.screenShot, new Rect (0, 0, Utilities.instance.screenShot.width, Utilities.instance.screenShot.height), new Vector2 (0.5f, 0.5f));
		facebookComposerPanel.SetActive (true);
		Utilities.instance.captureScreenShotCallback -= ScreenShotCallback;
	}

    public void LoginToFacebook() {
        FacebookManager.instance.LogIn();
    }
}
