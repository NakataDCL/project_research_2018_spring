using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFaceExpression : MonoBehaviour {

	FaceChanger faceChanger;

	// Use this for initialization
	void Start () {
		faceChanger = GetComponent<FaceChanger> ();
		if (faceChanger != null) {
			StartCoroutine ("randomChange");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator randomChange(){
		while (true) {
			int key,value;
			Dictionary<int,int> dictionary = new Dictionary<int,int> ();
			for (int i = 0; i < 3; i++) {
				while (dictionary.ContainsKey (key = Random.Range (0, 6))) {}
				value = Random.Range (0, 100);
				dictionary.Add (key, value);
			}
			faceChanger.changeExpression (dictionary);
			yield return new WaitForSeconds (1);
		}
	}
}
