using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour {
	const int EXPRESSION_HIDE = 0;
	const int EXPRESSION_SMILE = 1;
	const int EXPRESSION_ANGER = 2;
	const int EXPRESSION_SAD = 3;
	const int EXPRESSION_SURPRISED = 4;
	const int EXPRESSION_DAMAGED = 5;

	SkinnedMeshRenderer meshRenderer;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeExpression(Dictionary<int, int> dictionary){
		if (meshRenderer == null) {
			meshRenderer = GetComponent<SkinnedMeshRenderer>();
		}
		resetFace ();
		foreach(int key in dictionary.Keys){
			meshRenderer.SetBlendShapeWeight (key, dictionary [key]);
		}
	}

	private void resetFace(){
		for (int i = 0; i < 6; i++) {
			meshRenderer.SetBlendShapeWeight (i, 0);
		}
	}
}
