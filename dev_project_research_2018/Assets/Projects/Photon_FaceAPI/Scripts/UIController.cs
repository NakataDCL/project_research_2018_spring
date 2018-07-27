using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Text ui_anger;
	public Text ui_contempt;
	public Text ui_disgust;
	public Text ui_fear;
	public Text ui_happiness;
	public Text ui_neutral;
	public Text ui_sadness;
	public Text ui_surprise;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateUI(Dictionary<string, double> paramsDict){
		Debug.Log(paramsDict);
		ui_anger.text = paramsDict["anger"].ToString();
		ui_contempt.text = paramsDict["contempt"].ToString();
		ui_disgust.text = paramsDict["disgust"].ToString();
		ui_fear.text = paramsDict["fear"].ToString();
		ui_happiness.text = paramsDict["happiness"].ToString();
		ui_neutral.text = paramsDict["neutral"].ToString();
		ui_sadness.text = paramsDict["sadness"].ToString();
		ui_surprise.text = paramsDict["surprise"].ToString();
	}
	
}
