using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TouchEventHandler : MonoBehaviour {

//	private Vector2 oldPosition;
//	private float yRotation = 0.0f;
	private bool isTouched = false;
	private bool isOn;
	private Vector3 ScreenSpace;
	private Vector3 offset;
	private Vector3 touchposition;
	private GameObject renderCamer;
	Vector3 curScreenSpace;
	Vector3 CurPosition;


	// Use this for initialization
	void Start () {
		Input.multiTouchEnabled = true;
		renderCamer = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
//		AutoRotator ();
		isOn = GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().isOn;
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
			// 单指旋转
//			if (isOn) {
//				if (Input.touches [0].phase == TouchPhase.Began) {
//					
//				} else if (Input.touches [0].phase == TouchPhase.Moved) {
//					this.transform.Translate (new Vector3 (Input.touches [0].deltaPosition.x * 0.1f, Input.touches [0].deltaPosition.y * 0.3f, 0));
//				}
//				if (Input.touches [0].phase == TouchPhase.Began) { 
//					Debug.Log("down");
//				} else if (Input.touches [0].phase == TouchPhase.Moved) {
//					//将物体由世界坐标系转化为屏幕坐标系 ，由vector3 结构体变量ScreenSpace存储，以用来明确屏幕坐标系Z轴的位置  
//					ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);  
//					//完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系，2只有三维的情况下才能来计算鼠标位置与物体的距离，offset即是距离  
//					offset = transform.position-Camera.main.ScreenToWorldPoint(new Vector3(Input.touches[0].deltaPosition.x ,Input.touches[0].deltaPosition.y,ScreenSpace.z)); 
//					//得到现在鼠标的2维坐标系位置  
//					curScreenSpace =  new Vector3(Input.touches[0].deltaPosition.x,Input.touches[0].deltaPosition.y,ScreenSpace.z);     
//					//将当前鼠标的2维位置转化成三维的位置，再加上鼠标的移动量  
//					CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace)+offset;          
//					//CurPosition就是物体应该的移动向量赋给transform的position属性        
//					this.transform.position = CurPosition;
////					this.transform.Translate (new Vector3 (-Input.touches [0].deltaPosition.x * 0.1f, 0, -Input.touches [0].deltaPosition.y * 0.3f));
//				}
//				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
//					touchposition = Input.GetTouch (0).deltaPosition;
//					renderCamer.transform.Translate (-touchposition.x * 0.1f, -touchposition.y * 0.1f, 0);
//
////					gameObject.transform.localPosition += new Vector3 (touchposition.x, 0, touchposition.y);
//
//				}
//			} else {
				if (Input.touches [0].phase == TouchPhase.Began) {
				} else if (Input.touches [0].phase == TouchPhase.Moved) {
					this.transform.Rotate (new Vector3 (Input.touches [0].deltaPosition.y * 0.1f, -Input.touches [0].deltaPosition.x * 0.5f, 0), Space.World);
				}
//			}
		} else if (Input.touchCount > 1) {
			// 已脱卡则双指移动，否则双指缩放
			if (isOn) {
				if (Input.touches [0].phase == TouchPhase.Began || Input.touches [1].phase == TouchPhase.Began) {
				} else if (Input.touches [0].phase == TouchPhase.Moved || Input.touches [1].phase == TouchPhase.Moved) {
					touchposition = Input.GetTouch (0).deltaPosition;
					renderCamer.transform.Translate (-touchposition.x * 0.1f, -touchposition.y * 0.1f, 0);
//					this.transform.Rotate (new Vector3 (Input.touches [0].deltaPosition.y * 0.1f, -Input.touches [0].deltaPosition.x * 0.5f, 0), Space.World);
				}
			} else {
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
}
