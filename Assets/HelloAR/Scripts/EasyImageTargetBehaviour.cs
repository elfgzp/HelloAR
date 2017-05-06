/**
* Copyright (c) 2015-2016 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
* EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
* and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
*/

using UnityEngine;
using System.IO;
using System.Collections;

namespace EasyAR
{
    public class EasyImageTargetBehaviour : ImageTargetBehaviour, ITargetEventHandler
    {
		// 网络请求
		WWW wwwImageTarget;
		// 文件存储路径
		string filePath;
		string url = "";
		string lastUrl = "";
		
        protected override void Start()
        {
			// 现在只保存一张Target图
			filePath = Application.persistentDataPath + "/imageTarget.jpg";
            base.Start();
			// 取消隐藏，设置好Component后再调用隐藏
            // HideObjects(transform);
        }

		protected override void Update()
		{
			// 获取摄像头
			GameObject cameraDevice = GameObject.Find("CameraDevice");
			// 获取二维码中Target图片地址
			url = cameraDevice.GetComponent<EasyBarCodeScanner>().info.targetImageUrl;
			// 不是同一个二维码则开启协程加载Target图片
			if (url != "" && url != lastUrl) {
				StartCoroutine(LoadImageTarget());
				lastUrl = url;
			}
			// 回调(?)
			base.Update();
		}

		IEnumerator LoadImageTarget()
		{
			// 开始下载图片
			wwwImageTarget = new WWW(url);
			yield return wwwImageTarget;

			if (wwwImageTarget.error != null)
			{
				Debug.Log(wwwImageTarget.error);
				yield return null;
			}
			// 下载完成后保存图片到路径filePath
			byte[] bytes = wwwImageTarget.texture.EncodeToJPG();
			File.WriteAllBytes(filePath, bytes);
			SetupWithImage(filePath, StorageType.Absolute, "imageTarget", new Vector2());
			HideObjects(transform);
			// ShowObjects(transform);
		}

        void HideObjects(Transform trans)
        {
            for (int i = 0; i < trans.childCount; ++i)
                HideObjects(trans.GetChild(i));
            if (transform != trans)
                gameObject.SetActive(false);
        }

        public void ManualShowObjects()
        {
            ShowObjects(transform);
        }

        void ShowObjects(Transform trans)
        {
            for (int i = 0; i < trans.childCount; ++i)
                ShowObjects(trans.GetChild(i));
            if (transform != trans)
                gameObject.SetActive(true);
        }

        void ITargetEventHandler.OnTargetFound(Target target)
        {
            ShowObjects(transform);
            Debug.Log("Found: " + target.Id);
        }

        void ITargetEventHandler.OnTargetLost(Target target)
        {
            HideObjects(transform);
            Debug.Log("Lost: " + target.Id);
        }

        void ITargetEventHandler.OnTargetLoad(Target target, bool status)
        {
            Debug.Log("Load target (" + status + "): " + target.Id + " -> " + target.Name);
        }

        void ITargetEventHandler.OnTargetUnload(Target target, bool status)
        {
            Debug.Log("Unload target (" + status + "): " + target.Id + " -> " + target.Name);
        }
    }
}
