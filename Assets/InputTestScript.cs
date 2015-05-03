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
		c = GameObject.Find ("Canvas");
		
		Vector3 newPos = new Vector3(Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
		GameObject new_1 = (GameObject) Instantiate(c, newPos, Quaternion.identity);
		new_1.transform.position = newPos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H)) {//Overwrite onselect highlight
			Vector3 newPos = new Vector3(-1 * Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), -1 * Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), -1 * Sphere_Size * Mathf.Cos (0));
			GameObject new_1 = (GameObject) Instantiate(c, newPos, Quaternion.identity);
			new_1.transform.position = newPos;
		}
		//StartCoroutine ("ProcInput");
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
				} /*
				else if (Input.GetKeyDown(KeyCode.H)) {//Overwrite onselect highlight
					Vector3 newPos = new Vector3(-1 * Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), -1 * Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), -1 * Sphere_Size * Mathf.Cos (0));
					GameObject new_1 = (GameObject) Instantiate(c, newPos, Quaternion.identity);
					new_1.transform.position = newPos;
				}*/
						}
						yield return new WaitForSeconds (1f);
				}
		}
}
