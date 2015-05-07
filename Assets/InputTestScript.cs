using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using vrim;

public class InputTestScript : MonoBehaviour {
	Buffer b;
	GameObject rootCanvas;
	GameObject centerEyeAnchor;
	float Sphere_Size = 8.0F;
	Pointer p;
	InputField fileChooser;
	// Entrypoint of editor
	void Start () {
		centerEyeAnchor = GameObject.Find ("CenterEyeAnchor");
		rootCanvas = GameObject.Find ("Canvas");

		Vector3 newPos = new Vector3(Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
		rootCanvas.transform.position = newPos;
		InitBuffer (rootCanvas, null);

		GameObject go = GameObject.Find("EventSystem");
		p = (Pointer) go.GetComponent<Pointer>();

		// open file dialog
		fileChooser = (GameObject.Find ("FileDialog")).GetComponent<InputField>();
		fileChooser.Select ();
		fileChooser.ActivateInputField ();
		fileChooser.onValueChange.AddListener (FileDialogUpdate);
	}
	
	// Update is called once per frame
	void Update () {
		bool command = false;
		if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl)) {
			command = true;
		}
		if (command) {
			// zoom out
			if (Input.GetKey (KeyCode.Q)) {
				Sphere_Size = Sphere_Size - .3F;
				if(Sphere_Size < 5F){
					Sphere_Size = 5F;
				}
				GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
				for(int x = 0; x < a.GetLength(0); x++){
					GameObject cur = a[x];
					Vector3 vv = new Vector3(Sphere_Size * cur.transform.forward.x, Sphere_Size * cur.transform.forward.y, Sphere_Size * cur.transform.forward.z);
					cur.transform.position = vv;
				}
			} else if (Input.GetKey (KeyCode.E)) {
				Sphere_Size = Sphere_Size + .3F;
				if(Sphere_Size > 35F){
					Sphere_Size = 35F;
				}
				GameObject[] a = GameObject.FindGameObjectsWithTag("Player");
				for(int x = 0; x < a.GetLength(0); x++){
					GameObject cur = a[x];
					Vector3 vv = new Vector3(Sphere_Size * cur.transform.forward.x, Sphere_Size * cur.transform.forward.y, Sphere_Size * cur.transform.forward.z);
					cur.transform.position = vv;
				}
			} else if (Input.GetKeyDown(KeyCode.L)) {
				p.LockOn();
			} else if (Input.GetKeyDown(KeyCode.H)) {
				GameObject newBuf = (GameObject) Instantiate(rootCanvas, new Vector3(Sphere_Size * centerEyeAnchor.transform.forward.x, 
				                                                                     Sphere_Size * centerEyeAnchor.transform.forward.y, 
				                                                                     Sphere_Size * centerEyeAnchor.transform.forward.z), 
				                                             						 centerEyeAnchor.transform.rotation);
				SetBufText(newBuf, "");
				InitBuffer(newBuf, null);  
			}
		}

	}

	private static void InitBuffer(GameObject canvas, string filename) 
	{
		canvas.AddComponent<Buffer> ();
		Buffer b = (Buffer)canvas.GetComponent<Buffer> ();
		b.Init (canvas, filename);
	}

	private void FileDialogUpdate(string input)
	{
		if (!File.Exists (input))
			return;
		string text = File.ReadAllText (input);
		SetBufText (rootCanvas, text);
		GameObject fd = GameObject.Find ("FileDialog");
		if (fd == null)
			return;
		fd.SetActive (false);
	}

	private void SetBufText(GameObject buf, string text)
	{
		InputField inputfield = (InputField)(buf.GetComponentsInChildren (typeof(InputField))) [0];
		inputfield.text = text;
	}
		
}