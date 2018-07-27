using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FaceAPIManager : MonoBehaviour {
	private string url = "https://japaneast.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=false&returnFaceLandmarks=false&returnFaceAttributes=emotion";

	byte[] LoadBytes(string path){
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader bin = new BinaryReader(fs);
		byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
		bin.Close();
		return result;
	}

	private WebCameraManager _WebCamManager = null;
	private uint _interval = 0;

	void Start () {
		_WebCamManager = GameObject.Find("WebCameraManager").GetComponent<WebCameraManager>();
		if(_WebCamManager == null){
			Debug.Log("Can't find WebCameraManager.");
			return;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.Space) && _interval == 0){
			byte[] postData = _WebCamManager.getWebCamTextureBinary(); 
			Debug.Log(postData.Length);
			StartCoroutine("FaceAPIPost", postData);
			_interval = 300;
		}

		if(_interval > 0){
			_interval--;
			if(_interval == 0){
				Debug.Log("You can call face API.");
			}
		}
	}

	IEnumerator FaceAPIPost(byte[] postData){
		Debug.Log("call FaceAPIPost");

		if(postData == null || postData.Length <=0){
			yield return null;
		}
		//postData = LoadBytes("Assets/Resources/Images/pd.jpg");
		Debug.Log("Post Data: " + postData.Length + "[bytes]");

		// HTTP Header
		var headers = new Dictionary<string, string>(){
			{ "Ocp-Apim-Subscription-Key", "eee6fea1670f4b059b0b9aa95a37a951" },
			{ "Content-Type", "application/octet-stream"}
		};

		// サーバからJSON文字列取得
		WWW www = new WWW(url, postData, headers);

		yield return www;

		Debug.Log(www.text);
		//File.WriteAllBytes("Assets/Resources/Images/sc.png" ,postData);
	}
}
