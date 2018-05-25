using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMatchmaker : MonoBehaviour {
	public GameObject photonObject;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");

		// PhotonVoiceNetwork.Client.DebugEchoMode = false; が存在しない
		//PhotonVoiceNetwork.Client.SetDebugEchoMode();
	}
	
	// void OnJoinedLobby() が存在しない
	void OnJoinedLobby(){
		PhotonNetwork.JoinRandomRoom();
	}

	// void OnPhotonRandomJoinedFailed() が存在しない
	void OnPhotonRandomJoinedFailed(){
		PhotonNetwork.CreateRoom(null);
	}

	// OnJoinedRoom() が存在しない
	void OnJoinedRoom(){
		PhotonNetwork.Instantiate(
			photonObject.name,
			new Vector3(0.0f, 1.0f, 0.0f),
			Quaternion.identity,
			0
		);
		GameObject mainCamera = GameObject.FindWithTag("MainCamera");
		//mainCamera.GetComponent<ThirdPersonCamera>().enabled = true;
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
