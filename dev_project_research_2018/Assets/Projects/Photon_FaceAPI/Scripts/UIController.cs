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
		ui_anger.text     = "anger : " + paramsDict["anger"].ToString();
		ui_contempt.text  = "contempt : " + paramsDict["contempt"].ToString();
		ui_disgust.text   = "disgust : " + paramsDict["disgust"].ToString();
		ui_fear.text      = "fear : " + paramsDict["fear"].ToString();
		ui_happiness.text = "happiness : " + paramsDict["happiness"].ToString();
		ui_neutral.text   = "neutral : " + paramsDict["neutral"].ToString();
		ui_sadness.text   = "sadness : " + paramsDict["sadness"].ToString();
		ui_surprise.text  = "surprise : " + paramsDict["surprise"].ToString();
	}
	
}
