using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WebCameraController : MonoBehaviour {
	private int m_width = 640;
	private int m_height = 360;
	private WebCamTexture m_webCamTexture = null;
	Texture2D texture = null;
	private Color32[] colors;

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
		
		// 撮影開始
		m_webCamTexture.Play();
	}
	
	public WebCamTexture getWebCameraTexture(){
		return m_webCamTexture;
	}

	public byte[] getWebCamTextureBinary(){
		if(m_webCamTexture == null){
			return null;
		}

		// 画像をByte出力するための設定
		int width =  m_webCamTexture.width;
		int height = m_webCamTexture.height;

		// カメラの解像度に合わせる必要がある(自分のは1280*720)
		colors = new Color32[width * height];
        texture = new Texture2D (width, height, TextureFormat.RGBA32, false);
		
		m_webCamTexture.GetPixels32(colors);

		for (int x = 0; x < width; x++) {
        	for (int y = 0; y < height; y++) {
				Color c = colors [x + y * width];
				colors [x + y * width] = colors [x + y * width];
			}
		}

		texture.SetPixels32 (colors);
		texture.Apply ();

		return texture.EncodeToPNG();
	} 
}
