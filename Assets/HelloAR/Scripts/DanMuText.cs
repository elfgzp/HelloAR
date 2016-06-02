using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DanMuText : MonoBehaviour
{
	public void Reset(Queue<GameObject> Texts)
	{
		StartCoroutine("ShotDanMu", Texts);
	}

	IEnumerator ShotDanMu(Queue<GameObject> Texts)
	{
		float speed = 5f;
		while(transform.localPosition.x > -Screen.width / 2 - 50f)
		{
			transform.localPosition = new Vector3 (transform.localPosition.x - speed, transform.localPosition.y, transform.localPosition.z);
			yield return null;
		}
		Destroy (gameObject);
//		gameObject.SetActive(false);
//		Texts.Enqueue(gameObject);
		StopCoroutine ("ShotDanMu");
	}
}
