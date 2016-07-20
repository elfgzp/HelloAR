using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyHandler : MonoBehaviour {

	private GameObject webViewMask;
	private GameObject goBackBtn;

	private string goodsUrl = "";

	WebViewObject webViewObject;

	// Use this for initialization
	void Start () {

		goBackBtn = GameObject.FindGameObjectWithTag ("GoBack");
		webViewMask = GameObject.FindGameObjectWithTag ("WebView");

		goBackBtn.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, 32f);

		if (webViewMask != null) {
			webViewMask.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		goodsUrl = "https://ruter.github.io";
	}

	public void Buy (GameObject button)
	{
		Debug.Log ("GameObject " + button.name);
		webViewObject = (new GameObject ("WebViewObject")).AddComponent<WebViewObject> ();
		webViewObject.tag = "WebViewObject";
		webViewObject.Init (enableWKWebView: true);
		webViewObject.SetMargins (0, 32, 0, 0);
		webViewObject.SetVisibility (true);

		if (!webViewMask.activeSelf) {
			webViewMask.SetActive(true);
			webViewObject.transform.SetParent (webViewMask.transform);
			webViewObject.transform.SetAsFirstSibling ();
		}

		webViewObject.LoadURL (goodsUrl.Replace (" ", "%20"));
	}

	public void GoBack (GameObject button) {
		Debug.Log ("GameObject " + button.name);
		GameObject webViewObject = GameObject.FindGameObjectWithTag ("WebViewObject");
		webViewMask = GameObject.FindGameObjectWithTag ("WebView");
		Destroy (webViewObject);
		webViewMask.SetActive (false);
	}
}
