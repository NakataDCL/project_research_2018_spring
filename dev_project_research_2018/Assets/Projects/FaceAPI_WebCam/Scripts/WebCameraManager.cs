using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WebCameraManager : MonoBehaviour {
	private int m_width = 1280;
	private int m_height = 720;
	private WebCamTexture m_webCamTexture = null;
	Texture2D texture = null;
	private Color32[] colors;

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

		// 画像をByte出力するための設定
		colors = new Color32[m_width * m_height]; // カメラの解像度に合わせる必要がある(1280*720)
        texture = new Texture2D (m_width, m_height, TextureFormat.RGBA32, false);

		// 撮影開始
		m_webCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public WebCamTexture getWebCameraTexture(){
		return m_webCamTexture;
	}

	public byte[] getWebCamTextureBinary(){
		if(m_webCamTexture == null){
			return null;
		}

		// カメラの解像度に合わせる必要がある
		m_webCamTexture.GetPixels32(colors);

		Color32 rc = new Color32();
		for (int x = 0; x < m_width; x++) {
        	for (int y = 0; y < m_height; y++) {
				Color c = colors [x + y * m_width];
				colors [x + y * m_width] = colors [x + y * m_width];
			}
		}

		texture.SetPixels32 (colors);
		texture.Apply ();

		return texture.EncodeToPNG();
	} 
}
