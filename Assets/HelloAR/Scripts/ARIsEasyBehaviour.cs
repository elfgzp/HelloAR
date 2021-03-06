/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/

using UnityEngine;

namespace EasyAR
{
    public class ARIsEasyBehaviour : MonoBehaviour, ITargetEventHandler
    {
        [TextArea(1, 10)]
        public string Key;
        private const string title = "Pleaser enter KEY first!";
        private const string boxtitle = "===PLEASE ENTER YOUR KEY HERE===";
        private const string keyMessage = ""
            + "Steps to create the key for this sample:\n"
            + "  1. login www.easyar.com\n"
            + "  2. create app with\n"
            + "      Name: HelloAR (Unity)\n"
            + "      Bundle ID: cn.easyar.samples.unity.helloar\n"
            + "  3. find the created item in the list and show key\n"
            + "  4. replace all text in TextArea with your key";

		private GameObject scanLine;
		private GameObject scaleView;

		public GameObject tuoKa;

        private void Awake()
        {
            if(Key.Contains(boxtitle))
            {
#if UNITY_EDITOR
                UnityEditor.EditorUtility.DisplayDialog(title, keyMessage, "OK");
#endif
                Debug.LogError(title + " " + keyMessage);
            }
            ARBuilder.Instance.InitializeEasyAR(Key);
            ARBuilder.Instance.EasyBuild();
            foreach (var trackerBehaviour in ARBuilder.Instance.TrackerBehaviours)
                trackerBehaviour.RegisterTargetEventHandler(this);
        }

		void Start()
		{
			Screen.orientation = ScreenOrientation.AutoRotation;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;

			scanLine = GameObject.FindGameObjectWithTag ("ScanLine");
			scaleView = GameObject.FindGameObjectWithTag ("ScaleView");
//			scaleView.SetActive (false);
		}

        void ITargetEventHandler.OnTargetFound(Target target)
        {
            Debug.Log("<Global Handler> Found: " + target.Id);
			scanLine.SetActive (false);
			scaleView.SetActive (true);
//			if (GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().isOn) {
//				GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().isOn = false;
//				GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().TuoKaBtnClicked ();
//			}
        }

        void ITargetEventHandler.OnTargetLost(Target target)
        {
            Debug.Log("<Global Handler> Lost: " + target.Id);

			if (GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().isOn) {
				scanLine.SetActive (false);
//				scaleView.SetActive (true);
				GameObject.FindGameObjectWithTag ("TuoKa").GetComponent<TuoKaHandler> ().imgTarget.SetActive (true);
			} else {
				scanLine.SetActive (true);
				scaleView.SetActive (false);
			}
        }

        void ITargetEventHandler.OnTargetLoad(Target target, bool status)
        {
            Debug.Log("<Global Handler> Load target (" + status + "): " + target.Id + " -> " + target.Name);
        }

        void ITargetEventHandler.OnTargetUnload(Target target, bool status)
        {
            Debug.Log("<Global Handler> Unload target (" + status + "): " + target.Id + " -> " + target.Name);
        }
    }
}
