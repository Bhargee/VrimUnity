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
	Pointer p;
	GameObject go;
	InputField i;
	// Use this for initialization
	void Start () {
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

		go = GameObject.Find("EventSystem");
		p = (Pointer) go.GetComponent<Pointer>();
		//i.Select ();
		//i.ActivateInputField();
	}
	
	// Update is called once per frame
	void Update () {
		//StartCoroutine ("ProcInput");
		bool command = false;
		if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
			command = true;
		}
		if (command) {
			if (Input.GetKey (KeyCode.Q)) {
				Sphere_Size = Sphere_Size - .3F;
				Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
				c.transform.position = newPos;
			} else if (Input.GetKey (KeyCode.E)) {
				Sphere_Size = Sphere_Size + .3F;
				Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
				c.transform.position = newPos;
			} else if (Input.GetKey(KeyCode.L)) {
				p.LockOn();
			}
		}

	}
	
	IEnumerator ProcInput() 
	{
		while (true) {
			GameObject go;
			Pointer p;
			bool command = false;
			if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
				command = true;
			}
			if (command) {
				if (Input.GetKey (KeyCode.Q)) {
					Sphere_Size = Sphere_Size - .3F;
					Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
					c.transform.position = newPos;
				} else if (Input.GetKey (KeyCode.E)) {
					Sphere_Size = Sphere_Size + .3F;
					Vector3 newPos = new Vector3 (Sphere_Size * Mathf.Cos (0) * Mathf.Sin (0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
					c.transform.position = newPos;
				} else if (Input.GetKey(KeyCode.L)) {
					Debug.Log("Control L pressed");
					go = GameObject.Find("EventSystem");
					p = (Pointer) go.GetComponent<Pointer>();
					//p.LockOnObject();
				}
			}
			command = false;
			yield return new WaitForSeconds (.08f);
		}
	}
}
