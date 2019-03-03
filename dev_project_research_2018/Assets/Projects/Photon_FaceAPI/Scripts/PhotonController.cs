using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour {
	private PhotonView _PhotonViewControl;
	private UIController _UICon = null;
	private FaceChanger _utcFaceChanger = null;

	private void Awake(){ //初期化
		_PhotonViewControl = GetComponent<PhotonView>();
		//Inspectorに突っ込んでおいたComponentからPhotonViewを引っ張りだす
	}

	void Start()
	{
		_UICon = GameObject.Find("UIController").GetComponent<UIController>();
		if(_UICon == null){
			Debug.Log("Can't find UIController.");
			return;
		}

		//_utcFaceChanger = GameObject.Find("UTC").GetComponent<FaceChanger>();
		_utcFaceChanger = GameObject.Find("UTC_Face").GetComponent<FaceChanger>();
		if(_utcFaceChanger == null){
			Debug.Log("Can't find utc.");
			return;
		}
	}

	public void SendFaceParamToOthers( Dictionary<string,double> param ) {//送信メソッド
		_PhotonViewControl.RPC("SendFunc", PhotonTargets.Others, param );
		//RPC([使うメソッドの名前],[メソッドを発動させる対象],[メソッドの引数に突っ込む値])
		
		Debug.Log("Send: " + param);

		// for debug
		// アバターに反映
		Dictionary<int, int> faceparams = new Dictionary<int, int>();
		faceparams[0] = 0; // HIDE, 使用しない 
		faceparams[1] = (int)(param["happiness"]*100.0); // SMILE
		faceparams[2] = (int)(param["anger"]*100.0); // ANGER
		faceparams[3] = (int)(param["sadness"]*100.0); // SAD
		faceparams[4] = (int)(param["surprise"]*100.0); // surprised
		faceparams[5] = 0; // DAMAGED, 使用しない
		_utcFaceChanger.changeExpression(faceparams);
	}

	[PunRPC]//RPCで呼び出したいメソッドは、メソッドの前にこいつを必ず書いとく
	private void SendFunc(Dictionary<string,double> SendedData){//相手が送ってきたときに自動的に発動
		Debug.Log("Receive Data.");
		_UICon.updateUI(SendedData); //相手から来たデータを画面上に反映

		// アバターに反映
		Dictionary<int, int> faceparams = new Dictionary<int, int>();
		faceparams[0] = 0; // HIDE, 使用しない 
		faceparams[1] = (int)(SendedData["happiness"]*100.0); // SMILE
		faceparams[2] = (int)(SendedData["anger"]*100.0); // ANGER
		faceparams[3] = (int)(SendedData["sadness"]*100.0); // SAD
		faceparams[4] = (int)(SendedData["surprise"]*100.0); // surprised
		faceparams[5] = 0; // DAMAGED, 使用しない

		_utcFaceChanger.changeExpression(faceparams);
	}
}
