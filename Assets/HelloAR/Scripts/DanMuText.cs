using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DanMuText : MonoBehaviour
{
	public void Reset()
	{
		StartCoroutine("ShotDanMu");
	}

	IEnumerator ShotDanMu()
	{
		float speed = 5f;
		while(transform.localPosition.x > -Screen.width / 2 - 50f)
		{
			transform.localPosition = new Vector3 (transform.localPosition.x - speed, transform.localPosition.y, transform.localPosition.z);
			yield return null;
		}
		Destroy (gameObject);
//		gameObject.SetActive(false);
		StopCoroutine ("ShotDanMu");
	}
}
