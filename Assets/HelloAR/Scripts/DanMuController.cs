using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using EasyAR;
using JsonFx.Json;

/// <summary>
/// 实现看视频时的弹幕效果
/// </summary>
public class DanMuController : MonoBehaviour
{
	//public Text[] texts;
	public GameObject textPrefab;

	[Tooltip("满屏的个数")] public int num;

	public Queue<GameObject> Texts = new Queue<GameObject>();
//	private bool isDanmuOn = false;
	private IEnumerator coroutine;
	private string url = "";
	private GameObject cameraDevice;
	private bool isOutOfStock = false;

    int page = 1;
	string[] commentList = {};
    // GameObject.FindGameObjectWithTag ("CommView").GetComponent<OnButtonTouched> ().isShow

    // Use this for initialization
    void Awake()
    {
        cameraDevice = GameObject.Find("CameraDevice");
    }


    void Start()
	{

	}

	void Update()
	{
		url = cameraDevice.GetComponent<EasyBarCodeScanner> ().info.getCommentsApi;
	}

//	public void StartShot()
//	{
//		for (int i = 0; i < num; i++)
//		{
//			GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
//			obj.SetActive(false);
//			obj.transform.SetParent(transform);
//			obj.transform.localScale = Vector3.one;
//			Texts.Enqueue(obj);
//		}
//		coroutine = DanmuAnimation();
//		StartCoroutine(coroutine);
//	}
	public IEnumerator StartShot()
	{
		while (gameObject.activeSelf) {
			if (commentList.Length < 1) {
				StartCoroutine (GetComments ());
				if (isOutOfStock) {
					StopCoroutine ("StartShot");
				}
			}
			page += 1;
			yield return new WaitForSeconds (10f);
			StopCoroutine ("GetComments");
			StopCoroutine ("StowDanMu");
			StopCoroutine ("DanMuAnimation");
		}
	}
    // 弹幕发射移动
    public IEnumerator DanmuAnimation()       
	{
		while (Texts.Count > 0)
		{
				GameObject obj = Texts.Dequeue ();
				if (obj) {
					//				obj.transform.localPosition = new Vector3(Screen.width + 10f, Random.Range(-Screen.height / 2 + 50f, Screen.height / 2 - 120f));
					//				obj.GetComponent<Text>().text = DanMuStrings[Random.Range(0, DanMuStrings.Length)];
					//				obj.GetComponent<Text>().color = TextColors[Random.Range(0, TextColors.Length)];
					obj.SetActive (true);

					obj.GetComponent<DanMuText> ().Reset ();
				} else {
					yield break;
				}
				yield return new WaitForSeconds (1f);
		}
	}
	// 发射弹幕
	public void ShotDanMu(string danmu)
	{
		GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
		obj.SetActive(false);
		obj.transform.SetParent(transform);
		obj.transform.localScale = Vector3.one;

		obj.transform.localPosition = new Vector3(Screen.width + 10f, Random.Range(-Screen.height / 2 + 50f, Screen.height / 2 - 120f));
		obj.GetComponent<Text>().text = danmu;
		obj.GetComponent<Text>().color = TextColors[Random.Range(0, TextColors.Length)];
		obj.GetComponent<Text> ().fontSize = 25;
		obj.GetComponent<Text> ().fontStyle = FontStyle.BoldAndItalic;
		obj.SetActive(true);

		obj.GetComponent<DanMuText>().Reset();
	}
	// 获取弹幕
    public IEnumerator GetComments()
	{
		Debug.Log ("Get Comments...");
		if (url != "") {
			WWW wwwCommJson = new WWW (url + page);
			yield return wwwCommJson;

			if (wwwCommJson.error != null)
			{
				Debug.Log(wwwCommJson.error);
				yield break;
			}

			string json = wwwCommJson.text;
			if (json != "error") {
				commentList = JsonReader.Deserialize<string[]> (json);
                StartCoroutine(StowDanMu());
                StartCoroutine(DanmuAnimation());
                print(commentList);
			} else {
				isOutOfStock = true;
                page = 1;
			}
//			StopCoroutine ("GetComments");
		} else {
			yield break;
		}
	}
    // 装载弹幕
    public IEnumerator StowDanMu()
	{
		Debug.Log ("Stow Dan Mu...");
		foreach (string comment in commentList) {
			GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
			obj.SetActive(false);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;

			obj.transform.localPosition = new Vector3(Screen.width + 10f, Random.Range(-Screen.height / 2 + 50f, Screen.height / 2 - 120f));
			obj.GetComponent<Text>().text = comment;
			obj.GetComponent<Text>().color = TextColors[Random.Range(0, TextColors.Length)];

			Texts.Enqueue(obj);
		}
		yield return null;

		// 清空弹幕
		commentList = new string[]{};
//		for (int i = 0; i < commentList.Length; i++)
//		{
//			GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
//			obj.SetActive(false);
//			obj.transform.SetParent(transform);
//			obj.transform.localScale = Vector3.one;
//			Texts.Enqueue(obj);
//		}
	}
	/// <summary>
	/// 弹幕的开关
	/// </summary>
//	public void DanMuToggle(Toggle toggle)
//	{
//		if (toggle.isOn)
//		{
//			isDanmuOn = true;
//			StartCoroutine(coroutine);
//			Debug.Log("弹幕开！！！！！");
//		}
//		else
//		{
//			isDanmuOn = false;
////			DOTween.KillAll();
//			StopCoroutine(coroutine);
//			// Texts.Clear();
//			foreach (var text in transform.GetComponentsInChildren<DanMuText>())                  // 只能得到被激活的对象而已, 得不到全部？  所以不用Clear()直接把这些在外面的入队就行
//			{
//				Texts.Enqueue(text.gameObject);
//				text.gameObject.SetActive(false);
//			}
//			Debug.Log("弹幕关！！！！！个数: " + Texts.Count);
//		}
//	}

	[HideInInspector]
	public string[] DanMuStrings =
	{
		"这个有点意思",
		"好玩好玩！",
		"wow～好赞！",
		"感觉不错哈",
		"可以有可以有",
		"有点屌啊",
		"看起来很利害的样子",
		"火钳刘明",
		"我是一条弹幕是弹幕是一条弹幕",
		"什么什么？！还能这样玩！",
		"blahblahblahblah",
		"由我来组成弹幕～～～～～",
		"ZZZZZZZZZZZZZZZ",
		"XXXXXXXXXXXXXXXXXXX"
	};

	public Color[] TextColors;
}
