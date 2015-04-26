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
		StartCoroutine("ProcInput");
		c = GameObject.Find ("Canvas");
		Vector3 newPos = new Vector3(Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
		c.transform.position = newPos;
		c2 = GameObject.Find ("Canvas");
		Vector3 newPos2 = new Vector3(Sphere_Size * Mathf.Cos (Mathf.Atan(0/20)) * Mathf.Sin(Mathf.Acos(180/Sphere_Size)), Sphere_Size * Mathf.Sin (Mathf.Atan(0/20)) * Mathf.Sin (Mathf.Acos(180/Sphere_Size)), Sphere_Size * Mathf.Cos (Mathf.Acos(180/Sphere_Size)));
		c2.transform.position = newPos2;
		i = (GameObject.Find ("InputField")).GetComponent<InputField>();
		i.Select ();
		i.ActivateInputField();
	}
	
	// Update is called once per frame
	void Update () {

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
