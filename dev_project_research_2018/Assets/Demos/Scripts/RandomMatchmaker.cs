using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatchmaker : MonoBehaviour {
	public GameObject photonObject;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings(null);

		// PhotonVoiceNetwork.Client.DebugEchoMode = false; が存在しない
		//PhotonVoiceNetwork.Client.SetDebugEchoMode();
	}
	
	// ロビーに入ると呼ばれる
	void OnJoinedLobby(){
		Debug.Log("ロビーに入りました");

		// ルームに入室する
		PhotonNetwork.JoinRandomRoom();
	}

	// ルームに入手つすると呼ばれる
	void OnJoinedRoom(){
		Debug.Log("ルームへ入室しました。");

		PhotonNetwork.Instantiate(
			photonObject.name,
			new Vector3(0.0f, 1.0f, 0.0f),
			Quaternion.identity,
			0
		);
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		//mainCamera.GetComponent<ThirdPersonCamera>().enabled = true;
	}

	// ルームの入室に失敗すると呼ばれる
	void OnPhotonRandomJoinFailed(){
		Debug.Log("ルームの入室に失敗しました");

		// ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
		//PhotonNetwork.CreateRoom(null);
		PhotonNetwork.CreateRoom("myRoomName");
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
