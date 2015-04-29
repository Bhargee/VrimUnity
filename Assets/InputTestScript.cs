using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using vrim;

public class InputTestScript : MonoBehaviour {
	Buffer b;
	GameObject c;
	GameObject c2;
	Text t; 
	float Sphere_Size = 8.0F;

	InputField i;
	// Use this for initialization
	void Start () {
		b = new Buffer ();
		t = (GameObject.Find ("Text")).GetComponent<Text>();
		//StartCoroutine("ProcInput");
		c = GameObject.Find ("Canvas");
		Vector3 newPos = new Vector3(Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
		c.transform.position = newPos;
		int x, y, z;
		float r;
		x = 200;
		y = 200;
		z = 200;
		r = Mathf.Sqrt (x * x + y * y + z * z);
		c2 = GameObject.Find ("Canvas2");
		Vector3 newPos2 = new Vector3(r * Mathf.Cos (Mathf.Atan(x/y)) * Mathf.Sin(Mathf.Acos(z/r)), r * Mathf.Sin (Mathf.Atan(x/y)) * Mathf.Sin (Mathf.Acos(z/r)), r * Mathf.Cos (Mathf.Acos(z/r)));
		c2.transform.position = newPos2;
		c2.transform.Rotate (x, y, z);
		i = (GameObject.Find ("InputField")).GetComponent<InputField>();
		//i.Select ();
		//i.ActivateInputField();
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ("ProcInput");
	}

	IEnumerator ProcInput() 
	{
				while (true) {
						bool command = false;
						if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
								command = true;
						}
						if (command) {
								//Should probably set min and max
								//At least at 0
								//But who am I to limmit the user?
								if (Input.GetKey (KeyCode.Q)) {
										Sphere_Size = Sphere_Size - .3F;
										Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
										c.transform.position = newPos;
								} else if (Input.GetKey (KeyCode.E)) {
										Sphere_Size = Sphere_Size + .3F;
										Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
										c.transform.position = newPos;
				} else if (Input.GetKey (KeyCode.L)) {//Overwrite onselect highlight
					i = (GameObject.Find ("InputField")).GetComponent<InputField>();
					i.Select ();
					i.ActivateInputField();
				}else if (Input.GetKey (KeyCode.K)) {//Overwrite onselect highlight
					i = (GameObject.Find ("InputField2")).GetComponent<InputField>();
					i.Select ();
					i.ActivateInputField();
				}
						}
						yield return new WaitForSeconds (.08f);
				}
		}
}
