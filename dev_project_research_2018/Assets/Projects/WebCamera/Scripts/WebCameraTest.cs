using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCameraTest : MonoBehaviour {
	[SerializeField]
    private int m_width = 1920;
    [SerializeField]
    private int m_height = 1080;
	[SerializeField]
    private RawImage m_displayUI = null;

	private WebCamTexture m_webCamTexture = null;

	// Use this for initialization
	private IEnumerator Start () {
		
		// 接続されているカメラを探す
		if(WebCamTexture.devices.Length == 0){
			Debug.LogFormat("カメラが見つかりません");
			yield break;
		}

		// カメラの使用許可
		yield return Application.RequestUserAuthorization( UserAuthorization.WebCam );
		if( !Application.HasUserAuthorization( UserAuthorization.WebCam ) )
		{
			Debug.LogFormat( "カメラの使用が許可されていません。" );
			yield break;
		}

		// 最初に取得されたデバイスを使ってテクスチャを作る。
		// TODO: フレームレートの指定
        WebCamDevice userCameraDevice = WebCamTexture.devices[0];
        m_webCamTexture = new WebCamTexture( userCameraDevice.name, m_width, m_height );

		m_displayUI.texture = m_webCamTexture;

		// 撮影開始
		m_webCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPlay()
	{
		if( m_webCamTexture == null )
		{
			return;
		}

		if( m_webCamTexture.isPlaying )
		{
			return;
		}

		m_webCamTexture.Play();
	}

	public void OnStop()
	{
		if( m_webCamTexture == null )
		{
			return;
		}

		if( !m_webCamTexture.isPlaying )
		{
			return;
		}

		m_webCamTexture.Stop();
	}
}
