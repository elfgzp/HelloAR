using UnityEngine;
using System.Collections;


public class BarCodeInfo
{
	public string scanId { get; set;}
    public string targetImageUrl { get; set; }
    public string prefabUrl { get; set; }
	public string productLink { get; set;}
	public string commentApi { get; set; }
	public string getCommentsApi { get; set; }
	public string likeApi { get; set; }
	public string getLikesApi { get; set; }

	public BarCodeInfo() {
			scanId = "";
        	targetImageUrl = "";
        	prefabUrl = "";
			productLink = "";
			commentApi = "";
			getCommentsApi = "";
			likeApi = "";
			getLikesApi = "";
    }
}


