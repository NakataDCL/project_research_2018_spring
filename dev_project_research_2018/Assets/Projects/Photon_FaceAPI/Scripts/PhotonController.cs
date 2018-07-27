using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : MonoBehaviour {
	private PhotonView _PhotonViewControl;
	private UIController _UICon = null;

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
	}

	public void SendFaceParamToOthers( Dictionary<string,double> param ) {//送信メソッド
		_PhotonViewControl.RPC("SendFunc", PhotonTargets.Others, param );
		//RPC([使うメソッドの名前],[メソッドを発動させる対象],[メソッドの引数に突っ込む値])
		
		Debug.Log("Send: " + param);
	}

	[PunRPC]//RPCで呼び出したいメソッドは、メソッドの前にこいつを必ず書いとく
	private void SendFunc(Dictionary<string,double> SendedData){//相手が送ってきたときに自動的に発動

		_UICon.updateUI(SendedData); // //相手から来たデータを画面上に反映
	}
}
