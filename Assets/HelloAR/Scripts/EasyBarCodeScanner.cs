using UnityEngine;
using JsonFx.Json;
using System.Collections;
using System.Threading;

namespace EasyAR
{
    public class EasyBarCodeScanner : BarCodeScannerBehaviour
    {


        private string lastBarCode = "";
		private string json;
		private GameObject scanLine;
		private float y = 0f;
		private float scanViewHeight = 0f;
		// 二维码解析后的内容
		public BarCodeInfo info = new BarCodeInfo();
        static string prefabTag = "OldPrefab";
        public GameObject imageTarget;
		public bool isScanning = true;

        protected override void Start()
        {
			scanLine = GameObject.FindGameObjectWithTag ("ScanLine");
            imageTarget = GameObject.Find("ImageTarget");
            EnableOnStart = true;
			// 二维码被扫描到，事件BarCodeUpdate将会被触发
            BarCodeUpdate += OnBarCodeUpdate;
            base.Start();

			StartCoroutine (ScanLineAnimation());
			Debug.Log ("Start!");
			scanViewHeight = GameObject.FindGameObjectWithTag ("Scanner").GetComponent<RectTransform>().rect.height;
			y = scanViewHeight + 5;
        }

        private void OnBarCodeUpdate(BarCodeScannerBaseBehaviour scanner, string barcode)
        {
            Debug.Log("barcode: " + barcode);

			if (lastBarCode != barcode) {
                DeleteOldPrefab();
                StartCoroutine(GetJson(barcode));
            }
			lastBarCode = barcode;
        }

		void OnGUI() {
		}

		IEnumerator ScanLineAnimation()
		{
			while (isScanning) {
				yield return null;
				scanLine.GetComponent<RectTransform> ().localPosition = new Vector3(0, y - 5f, 0);
				y -= 5f;
				if (y < - 5) {
					y = scanViewHeight + 5;
				}
			}
		}

		// 解析二维码链接里的json
        IEnumerator GetJson(string url)
		{
            WWW www = new WWW(url + "&&is_ar_scanner=1");
            yield return www;

            json = www.text;
			if (json != "" && json != "error") {
				info = JsonReader.Deserialize<BarCodeInfo> (json);
				Debug.Log (info.targetImageUrl);
				Debug.Log (info.prefabUrl);
			} else {
				lastBarCode = "";
			}
        }

        void DeleteOldPrefab()
        {
            imageTarget.GetComponent<EasyImageTargetBehaviour>().ManualShowObjects();
            GameObject oldPrefab = GameObject.FindGameObjectWithTag(prefabTag);
            if (oldPrefab != null)
            {
                //			oldPrefab.tag = deletedTag;
                Destroy(oldPrefab);
                Debug.Log(oldPrefab);
            }
        }




    }
}
