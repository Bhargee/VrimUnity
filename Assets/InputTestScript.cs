using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
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

		GameObject go = GameObject.Find("EventSystem");
		p = (Pointer) go.GetComponent<Pointer>();

		// open file dialog
		fileChooser = (GameObject.Find ("FileDialog")).GetComponent<InputField>();
		fileChooser.Select ();
		fileChooser.ActivateInputField ();
		fileChooser.onValueChange.AddListener (FileDialogUpdate);
		fileChooser.transform.parent = rootCanvas.transform;
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
				GameObject fb = newBuf.transform.Find("FileDialog").gameObject;
				fb.active = true;
				InputField newFileChooser = fb.GetComponent<InputField>();
				newFileChooser.Select();
				newFileChooser.ActivateInputField();
				newFileChooser.onValueChange.AddListener(FileDialogUpdate);
				SetBufText(newBuf, "");
			} else if (Input.GetKeyDown(KeyCode.K)) {

			}
			return;
		}

	}

	private static void InitBuffer(GameObject canvas, string filename) 
	{
		canvas.AddComponent<Buffer> ();
		//GameObject ipf = new InputField ();
		Buffer b = (Buffer)canvas.GetComponent<Buffer> ();
		b.Init (canvas, filename);
	}

	private void FileDialogUpdate(string input)
	{
		if (!File.Exists (input))
			return;
		string text = File.ReadAllText (input);
		EventSystem curr = EventSystem.current;
		GameObject currFileDialog = curr.currentSelectedGameObject;
		GameObject currCanvas = currFileDialog.transform.parent.gameObject;
		SetBufText (currCanvas, text);
		SetBufText (currFileDialog, "");
		currFileDialog.SetActive (false);
	}

	private void SetBufText(GameObject buf, string text)
	{
		InputField inputfield = (InputField)(buf.GetComponentsInChildren (typeof(InputField))) [0];
		inputfield.text = text;
	}
		
}