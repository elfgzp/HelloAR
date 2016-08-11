using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EasyAR;

public class TuoKaHandler : MonoBehaviour {

	private GameObject cameraDevice;
	private GameObject renderCamera;
	private GameObject augmenter;
	private GameObject switchBtn;
	public GameObject imgTarget;
	private GameObject scanLine;

	public bool isOn = false;

	private string scanId = "";
	private string oldScanId = "";

	void Awake () 
	{
		cameraDevice = GameObject.Find("CameraDevice");
		renderCamera = GameObject.Find("RenderCamera");
		augmenter = GameObject.Find ("Augmenter");
	}

	// Use this for initialization
	void Start () {
		scanLine = GameObject.FindGameObjectWithTag ("ScanLine");

		switchBtn = GameObject.FindGameObjectWithTag ("TuoKa");

		switchBtn.GetComponent<RectTransform>().localPosition = new Vector3 (Screen.width / 2f - 40f, -120f, 0f);

//		AttachGyro();
	}
	
	// Update is called once per frame
	void Update () {
		scanId = cameraDevice.GetComponent<EasyBarCodeScanner> ().info.scanId;
		if (scanId != "" && scanId != oldScanId) {
			oldScanId = scanId;
			switchBtn.GetComponent<RectTransform>().sizeDelta = new Vector2 (80f, 80f);
			imgTarget = GameObject.FindGameObjectWithTag ("ImgTarget");
		} else if (scanId == "") {
			switchBtn.GetComponent<RectTransform>().sizeDelta = new Vector2 (0f, 0f);
		}
	}

	public void TuoKaBtnClicked() {
		isOn = !isOn;
		if (isOn) {
			switchBtn.GetComponent<UnityEngine.UI.Image> ().material = Resources.Load ("Materials/Off") as Material;
			TuoKa ();
		} else {
			switchBtn.GetComponent<UnityEngine.UI.Image> ().material = Resources.Load ("Materials/On") as Material;
			TuoKaBack ();
		}
	}

	void TuoKa() {
		// target set active；hide scan line
		augmenter.GetComponent<AugmenterBehaviour> ().WorldCenter = AugmenterBaseBehaviour.CenterMode.Augmenter;
		GameObject imageTargetRoot = GameObject.FindGameObjectWithTag("ImageTargetRoot");
		imgTarget = imageTargetRoot.transform.Find ("ImageTarget").gameObject;
		imgTarget.SetActive (true);
		GameObject perfab = GameObject.FindGameObjectWithTag ("OldPrefab");
		Vector3 newPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform.localPosition + new Vector3 (0, -100, 0);
		perfab.transform.localPosition = newPosition;
		perfab.transform.localScale = new Vector3 (perfab.transform.localScale.x * 100, perfab.transform.localScale.y * 100, perfab.transform.localScale.x * 100);
		scanLine.SetActive (false);
		renderCamera.GetComponent<GyroHandler> ().AttachGyro ();
	}

	void TuoKaBack() {
		// target set deactive；
		augmenter.GetComponent<AugmenterBehaviour> ().WorldCenter = AugmenterBaseBehaviour.CenterMode.Target;
		GameObject perfab = GameObject.FindGameObjectWithTag ("OldPrefab");
		perfab.transform.localScale = new Vector3 (perfab.transform.localScale.x / 100, perfab.transform.localScale.y / 100, perfab.transform.localScale.x / 100);
		imgTarget.SetActive (false);
		scanLine.SetActive (true);
		renderCamera.GetComponent<GyroHandler> ().DetachGyro ();
		renderCamera.GetComponent<Transform> ().rotation = new Quaternion (0, 0, 0, 0);
	}
}
