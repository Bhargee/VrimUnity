using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using vrim;

public class InputTestScript : MonoBehaviour {
	Buffer b;
	Canvas c;
	Text t; 
	// Use this for initialization
	void Start () {
		b = new Buffer ();
		t = (GameObject.Find ("Text")).GetComponent<Text>();
		StartCoroutine("ProcInput");
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator ProcInput() 
	{
		while (true) {
			/* Editor Functions */
			// TODO
			string toInput = null;
			bool uppercase = false;
			if((Input.GetKey (KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
				uppercase = true;
			if (Input.GetKey(KeyCode.A)) toInput = "a";
			else if (Input.GetKey(KeyCode.B)) toInput = "b";
			else if (Input.GetKey(KeyCode.C)) toInput = "c";
			else if (Input.GetKey(KeyCode.C)) toInput = "c";
			else if (Input.GetKey(KeyCode.D)) toInput = "d";
			else if (Input.GetKey(KeyCode.E)) toInput = "e";
			else if (Input.GetKey(KeyCode.F)) toInput = "f";
			else if (Input.GetKey(KeyCode.G)) toInput = "g";
			else if (Input.GetKey(KeyCode.H)) toInput = "h";
			else if (Input.GetKey(KeyCode.I)) toInput = "i";
			else if (Input.GetKey(KeyCode.J)) toInput = "j";
			else if (Input.GetKey(KeyCode.K)) toInput = "k";
			else if (Input.GetKey(KeyCode.L)) toInput = "l";
			else if (Input.GetKey(KeyCode.M)) toInput = "m";
			else if (Input.GetKey(KeyCode.N)) toInput = "n";
			else if (Input.GetKey(KeyCode.O)) toInput = "o";
			else if (Input.GetKey(KeyCode.P)) toInput = "p";
			else if (Input.GetKey(KeyCode.Q)) toInput = "q";
			else if (Input.GetKey(KeyCode.R)) toInput = "r";
			else if (Input.GetKey(KeyCode.S)) toInput = "s";
			else if (Input.GetKey(KeyCode.T)) toInput = "t";
			else if (Input.GetKey(KeyCode.U)) toInput = "u";
			else if (Input.GetKey(KeyCode.V)) toInput = "v";
			else if (Input.GetKey(KeyCode.W)) toInput = "w";
			else if (Input.GetKey(KeyCode.X)) toInput = "x";
			else if (Input.GetKey(KeyCode.Y)) toInput = "y";
			else if (Input.GetKey(KeyCode.Z)) toInput = "z";

			if(uppercase && toInput != null)
				toInput = toInput.ToUpper();
			if (Input.GetKey(KeyCode.Space))
				toInput = " ";
			if (Input.GetKey(KeyCode.Return))
				toInput = "\n";
			   
			
			if (toInput != null) {
				b.Insert(toInput);
				t.text = b.GetContents();
			}
			yield return new WaitForSeconds (.08f);
		}
	}
}
