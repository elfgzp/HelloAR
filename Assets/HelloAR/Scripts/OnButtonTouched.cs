﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class OnButtonTouched : MonoBehaviour {

	private GameObject commView;
	private GameObject shotField;
	private GameObject textField;
	private GameObject shotButton;
	private GameObject commButton;
	private AsyncOperation async = null;
	private float commX = Screen.width + 50f;

	public bool isShow = false;

	void Awake () 
	{	
		
	}

	void Update ()
	{
		
	}

	void Start() {
		Debug.Log ("OnButtonTouched Start!");
		commView = GameObject.FindGameObjectWithTag("CommView");
		shotField = GameObject.FindGameObjectWithTag ("ShotField");
		textField = GameObject.FindGameObjectWithTag ("TextField");
		shotButton = GameObject.FindGameObjectWithTag ("ShotButton");
		commButton = GameObject.FindGameObjectWithTag ("CommButton");
		// 设置评论视图
		if (commView != null) {
			commView.GetComponent<RectTransform> ().localPosition = new Vector3 (Screen.width + 10f, 0f, 0f);
			Debug.Log ("I find it!");
			shotField.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, 80f);

			textField.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 3 * 2, 64f);
			textField.GetComponent<RectTransform> ().localPosition = new Vector3 (- Screen.width / 6 + 8, 40f, 0f);

			shotButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 3 - 64, 64f);
			shotButton.GetComponent<RectTransform> ().localPosition = new Vector3 (Screen.width / 3 - 20, 40f, 0f);
//			shotButton.GetComponent<RectTransform> ().localPosition = new Vector3 (Screen.width / 6 * 5, 8f, 0f);
//			commView.SetActive (false);
		}
	}

	void OnGUI()
	{
		// 设置评论视图
		if (commView != null) {
			Vector2 newSize = new Vector2(Screen.width + 10f, Screen.height + 10f);
			commView.GetComponent<RectTransform> ().sizeDelta = newSize;
		}
	}

	// 按钮的点击事件
	public void ScanButtonClick (GameObject button)
	{
		Debug.Log("GameObject " + button.name);
		//开始加载场景
		StartCoroutine("ScanScene");
	}

	public void QuitButtonClick (GameObject button)
	{
		Debug.Log("GameObject " + button.name);
		//开始加载场景
		StartCoroutine("MainScene");
	}

	public void CommBunttonClick (GameObject button)
	{
		Debug.Log ("GameObject " + button.name);

		commView.SetActive (true);

		if (isShow) {
			StartCoroutine ("CloseCommView");
			isShow = false;
		} else {
			StartCoroutine ("ShowCommView");
			isShow = true;

			commView.GetComponent<DanMuController> ().StartShot ();
		}
	}

	public void ShotButtonClick (GameObject button)
	{
		Debug.Log ("GameObject " + button.name);
		string danmu = textField.GetComponent<InputField> ().text;
		Debug.Log (danmu);

		textField.GetComponent<InputField> ().text = "";

		commView.GetComponent<DanMuController> ().ShotDanMu (danmu);
	}

	//异步加载
	IEnumerator ScanScene()
	{
		async = SceneManager.LoadSceneAsync ("HelloAR");
		// 读取完毕后返回，系统会自动进入场景
		yield return async;
	}

	IEnumerator MainScene ()
	{
		async = SceneManager.LoadSceneAsync ("Main");
		// 读取完毕后返回，系统会自动进入场景
		yield return async;
	}

	IEnumerator ShowCommView()
	{
		while(commX > 0)
		{
			yield return null;
			commX -= 45f;
			commView.GetComponent<RectTransform> ().localPosition = new Vector3(commX, 0f, 0f);
		}
		commX = 0f;
		commView.GetComponent<RectTransform> ().localPosition = new Vector3(commX, 0f, 0f);

		StopCoroutine ("ShowCommView");
	}

	IEnumerator CloseCommView()
	{
		while(commX < Screen.width)
		{
			yield return null;
			commX += 60f;
			commView.GetComponent<RectTransform> ().localPosition = new Vector3(commX, 0f, 0f);
		}
		commX = Screen.width + 50f;
		commView.GetComponent<RectTransform> ().localPosition = new Vector3(commX, 0f, 0f);

		GameObject[] danmus = GameObject.FindGameObjectsWithTag ("DanPfb");
		foreach (GameObject danmu in danmus)
		{
			Destroy (danmu);
		}

		commView.SetActive (false);

		StopCoroutine ("CloseCommView");
	}
}