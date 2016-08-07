using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EasyAR;

public class BuyHandler : MonoBehaviour {

	private GameObject webViewMask;
	private GameObject goBackBtn;
	private GameObject cameraDevice;
	private GameObject buyBtn;

	private string goodsUrl = "";
	private string oldGoodsUrl = "";

	WebViewObject webViewObject;

	void Awake () 
	{
		cameraDevice = GameObject.Find("CameraDevice");
	}

	// Use this for initialization
	void Start () {

		buyBtn = GameObject.FindGameObjectWithTag ("Buy");
		goBackBtn = GameObject.FindGameObjectWithTag ("GoBack");
		webViewMask = GameObject.FindGameObjectWithTag ("WebView");

//		buyBtn.SetActive (false);

		buyBtn.GetComponent<RectTransform>().localPosition = new Vector3 (Screen.width / 2f - 40f, 0f, 0f);
//		Debug.Log("*****localPosition: " + buyBtn.GetComponent<RectTransform>().localPosition + "*****\n");
		goBackBtn.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, 32f);
		goBackBtn.GetComponent<RectTransform>().localPosition = new Vector3 (0f, Screen.height / 2f, 0f);
//		Debug.Log("-*-BackBtn localPosition: " + goBackBtn.GetComponent<RectTransform>().localPosition + "-*-\n");

		if (webViewMask != null) {
			webViewMask.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
//		goodsUrl = "https://www.bing.com";
		goodsUrl = cameraDevice.GetComponent<EasyBarCodeScanner> ().info.productLink;
		if (goodsUrl != "" && goodsUrl != oldGoodsUrl) {
			oldGoodsUrl = goodsUrl;
			buyBtn.GetComponent<RectTransform>().sizeDelta = new Vector2 (80f, 80f);
		} else if (goodsUrl == "") {
			buyBtn.GetComponent<RectTransform>().sizeDelta = new Vector2 (0f, 0f);
		}
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
