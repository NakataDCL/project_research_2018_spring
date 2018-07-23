using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FaceAPITest : MonoBehaviour {

	byte[] LoadBytes(string path){
		FileStream fs = new FileStream(path, FileMode.Open);
		BinaryReader bin = new BinaryReader(fs);
		byte[] result = bin.ReadBytes((int)bin.BaseStream.Length);
		bin.Close();
		return result;
	}

	// Use this for initialization
	IEnumerator Start () {
		// Face API を呼ぶためのURL
		var url = "https://japaneast.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=true&returnFaceAttributes=gender,age";

		// 投げる画像(Byteに変換)
		byte[] bytes = LoadBytes("Assets/Resources/Images/pd.jpg");
		if(bytes.Length <= 0) {
			Debug.Log("Can't load image file.");
			yield return null;
		}else{
			Debug.Log("Success to load image file. " + bytes.Length + "[bytes]");
		}

		// HTTP Header
		var headers = new Dictionary<string, string>(){
			{ "Ocp-Apim-Subscription-Key", "eee6fea1670f4b059b0b9aa95a37a951" },
			{ "Content-Type", "application/octet-stream"}
		};

		// サーバからJSON文字列取得
		WWW www = new WWW(url, bytes, headers);

		yield return www;

		Debug.Log(www.text);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
