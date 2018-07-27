using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
	[SerializeField]
	private RawImage m_displayUI = null;

	private WebCameraManager _WebCamManager = null;

	// Use this for initialization
	void Start () {
		_WebCamManager = GameObject.Find("WebCameraManager").GetComponent<WebCameraManager>();
		if(_WebCamManager == null){
			Debug.Log("Can't find WebCameraManager.");
			return;
		}

		m_displayUI.texture = _WebCamManager.getWebCameraTexture();
	}
	
	// Update is called once per frame
	void Update () {
		m_displayUI.texture = _WebCamManager.getWebCameraTexture();
	}
}
