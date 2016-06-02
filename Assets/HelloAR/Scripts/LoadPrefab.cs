using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAR;
using JsonFx.Json;

public class LoadPrefab : MonoBehaviour
{
    public string url = "";
	public int version;
	AssetBundle bundle;

	private int versionTemp;
    private string lastUrl = "";
	private Dictionary<string, int> loadedBundles;

	static string prefabTag = "OldPrefab";

    void Start()
    {
		versionTemp = 0;
		loadedBundles = new Dictionary<string, int>();
    }

    void Update() 
	{
        GameObject cameraDevice = GameObject.Find("CameraDevice");
		// 获取二维码中模型的地址
		url = cameraDevice.GetComponent<EasyBarCodeScanner>().info.prefabUrl;

        if(url != "" && url != lastUrl) {
			version = CheckBundleVersion(url);
            // Destroy old prefab
            if (lastUrl !="")
            {
                DeleteOldPrefab();
            }

			StartCoroutine (DownLoadPrefab ());

			
            lastUrl = url;
        }
    }

    IEnumerator DownLoadPrefab() {
		// 等待下载完成
		yield return StartCoroutine (AssetBundleManager.downloadAssetBundle (url, version));

		bundle = AssetBundleManager.getAssetBundle (url, version);

		InstaObj ();
	}

	void InstaObj()
	{
            ShowObjects(transform);
        // 实例化模型对象
            GameObject gameObject = (GameObject)Instantiate(bundle.mainAsset, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            // 添加触摸事件的脚本
            gameObject.AddComponent<TouchEventHandler>();
            // 添加tag
            gameObject.tag = prefabTag;
            // 记录transform
            Vector3 originScale = gameObject.transform.localScale;
            // 设置该对象到子层级
            gameObject.transform.parent = transform;
            // 设置现在的缩放比例
            gameObject.transform.localScale = originScale;


    }

    void DeleteOldPrefab()
    {
        ShowObjects(transform);
        GameObject oldPrefab = GameObject.FindGameObjectWithTag(prefabTag);
        if (oldPrefab != null)
        {
            //			oldPrefab.tag = deletedTag;
            Destroy(oldPrefab);
            Debug.Log(oldPrefab);
        }
        int ver = CheckBundleVersion(lastUrl);
        AssetBundleManager.Unload(lastUrl, ver, true);
    }

    int CheckBundleVersion(string url)
	{
		if (loadedBundles.ContainsKey (url)) {
			return loadedBundles [url];
		} else {
			versionTemp += 1;
			loadedBundles.Add (url, versionTemp);
			return versionTemp;
		}
	}

    void ShowObjects(Transform trans)
    {
        for (int i = 0; i < trans.childCount; ++i)
            ShowObjects(trans.GetChild(i));
        if (transform != trans)
            gameObject.SetActive(true);
    }
}