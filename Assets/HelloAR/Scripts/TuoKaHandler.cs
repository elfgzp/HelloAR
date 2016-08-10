using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EasyAR;

public class TuoKaHandler : MonoBehaviour {

	private GameObject cameraDevice;
	private GameObject switchBtn;
	private GameObject imgTarget;
	private GameObject scanLine;

	private bool isOn = false;

	private string scanId = "";
	private string oldScanId = "";

	void Awake () 
	{
		cameraDevice = GameObject.Find("CameraDevice");
	}

	// Use this for initialization
	void Start () {
		scanLine = GameObject.FindGameObjectWithTag ("ScanLine");

		switchBtn = GameObject.FindGameObjectWithTag ("TuoKa");

		switchBtn.GetComponent<RectTransform>().localPosition = new Vector3 (Screen.width / 2f - 40f, -120f, 0f);
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
//		imgTarget = GameObject.FindGameObjectWithTag ("ImgTarget");
		imgTarget.SetActive (true);
		scanLine.SetActive (false);

	}

	void TuoKaBack() {
		// target set deactive；
//		imgTarget = GameObject.FindGameObjectWithTag ("ImgTarget");
		imgTarget.SetActive (false);
		scanLine.SetActive (true);
	}
}
