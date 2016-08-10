using UnityEngine;
using System.Collections;

public class TouchEventHandler : MonoBehaviour {

//	private Vector2 oldPosition;
//	private float yRotation = 0.0f;
	private bool isTouched = false;

	// Use this for initialization
	void Start () {
		Input.multiTouchEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
//		AutoRotator ();
		TouchEventMethod ();
	}

	private void AutoRotator()
	{
		if (!isTouched) {
//			yRotation -= Time.deltaTime * 30;
			transform.Rotate (new Vector3 (0, 10, 0) * Time.deltaTime);
		} else {
//			yRotation = this.transform.rotation.y;
			return;
		}
	}

	private void TouchEventMethod ()
	{
		if (Input.touchCount <= 0) {
			isTouched = false;
			return;
		}

		isTouched = true;
		if (Input.touchCount == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
//				oldPosition = Input.touches [0].position;
			} else if (Input.touches [0].phase == TouchPhase.Moved) {
				this.transform.Rotate (new Vector3 (Input.touches [0].deltaPosition.y * 0.1f, -Input.touches [0].deltaPosition.x * 0.5f, 0), Space.World);
//				this.transform.Rotate (new Vector3 (0, -Input.touches [0].deltaPosition.x * 0.5f, 0), Space.World);
			}
		} else if (Input.touchCount > 1) {
			Vector2 pos1 = new Vector2 ();
			Vector2 pos2 = new Vector2 ();

			Vector2 mov1 = new Vector2 ();
			Vector2 mov2 = new Vector2 ();

			for (int i = 0; i < 2; i++) {
				Touch touch = Input.touches[i];
				if (touch.phase == TouchPhase.Ended)
					break;
				if (touch.phase == TouchPhase.Moved) {
					float mov = 0;
					if (i == 0) {
						pos1 = touch.position;
						mov1 = touch.deltaPosition;
					} else {
						pos2 = touch.position;
						mov2 = touch.deltaPosition;
					}
					if (pos1.x > pos2.x)
						mov = mov1.x;
					else
						mov = mov2.x;

					if (pos1.y > pos2.y)
						mov += mov1.y;
					else
						mov += mov2.y;

					Camera.main.transform.Translate (0, 0, mov * Time.deltaTime * 0.75f);
				}
			}
		}
	}
}
