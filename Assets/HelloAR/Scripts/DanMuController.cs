using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
	// GameObject.FindGameObjectWithTag ("CommView").GetComponent<OnButtonTouched> ().isShow

	// Use this for initialization
	void Start()
	{
		
	}

	public void StartShot()
	{
		for (int i = 0; i < num; i++)
		{
			GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
			obj.SetActive(false);
			obj.transform.SetParent(transform);
			obj.transform.localScale = Vector3.one;
			Texts.Enqueue(obj);
		}
		coroutine = DanmuAnimation();
		StartCoroutine(coroutine);
	}

	private IEnumerator DanmuAnimation()       
	{
		while (true)
		{
			GameObject obj = Texts.Dequeue();
			if (obj)
			{
				obj.transform.localPosition = new Vector3(Screen.width + 10f, Random.Range(-Screen.height / 2 + 50f, Screen.height / 2 - 120f));
				obj.GetComponent<Text>().text = DanMuStrings[Random.Range(0, DanMuStrings.Length)];
				obj.GetComponent<Text>().color = TextColors[Random.Range(0, TextColors.Length)];
				obj.SetActive(true);

				obj.GetComponent<DanMuText>().Reset(Texts);
			}
			yield return new WaitForSeconds(0.2f);                
		}
	}

	public void ShotDanMu(string danmu)
	{
		GameObject obj = (GameObject)Instantiate(textPrefab, transform.position, Quaternion.identity);
		obj.SetActive(false);
		obj.transform.SetParent(transform);
		obj.transform.localScale = Vector3.one;

		obj.transform.localPosition = new Vector3(Screen.width + 10f, Random.Range(-Screen.height / 2 + 50f, Screen.height / 2));
		obj.GetComponent<Text>().text = danmu;
		obj.GetComponent<Text>().color = TextColors[Random.Range(0, TextColors.Length)];
		obj.GetComponent<Text> ().fontSize = 25;
		obj.GetComponent<Text> ().fontStyle = FontStyle.BoldAndItalic;
		obj.SetActive(true);

		obj.GetComponent<DanMuText>().Reset(Texts);
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
//	public string[] DanMuStrings =
//	{
//		"这个剧情也太雷人了吧！",
//		"还是好莱坞的电影经典啊，这个太次了",
//		"是电锯惊魂的主角，尼玛",
//		"这个游戏还是很良心的么",
//		"卧槽还要花钱，这一关也太难了",
//		"这个游戏好棒偶",
//		"全是傻逼",
//		"求约：13122785566",
//		"最近好寂寞啊，还是这个游戏好啊",
//		"难道玩游戏还能撸",
//		"办证：010 - 888888",
//		"为什么女主角没有死？",
//		"好帅呦，你这个娘们儿",
//		"欠揍啊，东北人不知道啊",
//		"我去都是什么人啊，请文明用语",
//		"这个还是不错的",
//		"要是胸再大点就更好了",
//		"这个游戏必须顶啊",
//		"怎么没有日本动作爱情片中的角色呢？",
//		"好吧，这也是醉了！",
//		"他只想做一个安静的美男子！"
//	};

	public Color[] TextColors;
}
