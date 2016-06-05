using UnityEngine;
using System.Collections;

public class LoadController : MonoBehaviour {

	[SerializeField]
	public GameObject loadPrefab;

	// Use this for initialization
	void Start () {
		Loading ();
		ScaleController ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Loading()
	{
		GameObject obj = (GameObject)Instantiate(loadPrefab, transform.position, Quaternion.identity);
		obj.SetActive(false);
		obj.transform.SetParent(transform);
		obj.transform.localScale = Vector3.zero;
		obj.SetActive (true);
	}

	public void ScaleController()
	{
		GameObject scaleView = GameObject.FindGameObjectWithTag ("ScaleView");
//		scaleView.transform.localScale = Vector3.zero;
		scaleView.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width / 2, 64f);
		scaleView.transform.localPosition = new Vector3 (0f, -Screen.height / 2 + 32, 0f);
	}
}
