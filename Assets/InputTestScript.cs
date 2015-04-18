using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using vrim;

public class InputTestScript : MonoBehaviour {
	Buffer b;
	Canvas c;
	// Use this for initialization
	void Start () {
		b = new Buffer ();
		//GameObject go = GameObject.Find ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Text t = (GameObject.Find ("Text")).GetComponent<Text>();
			b.Insert(Input.inputString);
			t.text = b.GetContents();
		}
	}
}
