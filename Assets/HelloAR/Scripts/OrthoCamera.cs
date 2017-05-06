using UnityEngine;
using System.Collections;

public class OrthoCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SetupOrthographicSize ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetupOrthographicSize() {
		// Get screen height
		Camera.main.orthographicSize = Screen.height / 100f / 2f;
	}
}
