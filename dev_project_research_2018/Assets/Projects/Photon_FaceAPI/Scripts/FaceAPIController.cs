using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class FaceAPIController : MonoBehaviour {

	// Use this for initialization
	private string url = "https://japaneast.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=false&returnFaceLandmarks=false&returnFaceAttributes=emotion";

	private UIController _UICon = null;
	private WebCameraController _WebCamCon = null;
	private const uint _interval = 300; // Face APIの呼び出し間隔 [frame]

	private uint _cooltime = 0;
	
	void Start () {
		_WebCamCon = GameObject.Find("WebCameraController").GetComponent<WebCameraController>();
		if(_WebCamCon == null){
			Debug.Log("Can't find WebCameraController.");
			return;
		}

		_UICon = GameObject.Find("UIController").GetComponent<UIController>();
		if(_UICon == null){
			Debug.Log("Can't find UIController.");
			return;
		}
	}

	void FixedUpdate () {
		if(_WebCamCon != null && _cooltime == 0){
			byte[] postData = _WebCamCon.getWebCamTextureBinary();
			if(postData != null){ 
				Debug.Log(postData.Length);
				StartCoroutine("FaceAPIPost", postData);
				_cooltime = 300;
			}
		}

		if(_cooltime > 0){
			if(--_cooltime == 0){
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

		// FaceAPIのレスポンスから表情のパラメータを取り出す
		Dictionary<string, double> paramsDict  = parseFaceAPIResponse(www.text);
		_UICon.updateUI(paramsDict);
	}

	// FaceAPIのレスポンスから表情のパラメータを取り出す
	public Dictionary<string, double> parseFaceAPIResponse(string json){
		var jsonResultArray = (IList) Json.Deserialize (json);
		var jsonResult = (IDictionary)jsonResultArray[0];
		var jsonFaceAttributes = (IDictionary)jsonResult["faceAttributes"];
		var jsonEmotion = (IDictionary)jsonFaceAttributes["emotion"];
		Dictionary<string, double> paramsDict  = new Dictionary<string, double>();
		paramsDict["anger"] = (double)jsonEmotion["anger"];
		paramsDict["contempt"] = (double)jsonEmotion["contempt"];
		paramsDict["disgust"] = (double)jsonEmotion["disgust"];
		paramsDict["fear"] = (double)jsonEmotion["fear"];
		paramsDict["happiness"] = (double)jsonEmotion["happiness"];
		paramsDict["neutral"] = (double)jsonEmotion["neutral"];
		paramsDict["sadness"] = (double)jsonEmotion["sadness"];
		paramsDict["surprise"] = (double)jsonEmotion["surprise"];

		return paramsDict;
	}
}
