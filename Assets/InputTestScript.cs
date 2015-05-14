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
	float Sphere_Size = 12.0F;
	Pointer p;
	InputField fileChooser;
	// Entrypoint of editor
	void Start () {
		centerEyeAnchor = GameObject.Find ("CenterEyeAnchor");
		rootCanvas = GameObject.Find ("Canvas");
		//InputField ipf = rootCanvas.GetComponent<InputField> ();
		//ipf.caretBlinkRate = 3f;

		Vector3 newPos = new Vector3(Sphere_Size * Mathf.Cos (0) * Mathf.Sin(0), Sphere_Size * Mathf.Sin (0) * Mathf.Sin (0), Sphere_Size * Mathf.Cos (0));
		rootCanvas.transform.position = newPos;

		GameObject go = GameObject.Find("EventSystem");
		p = (Pointer) go.GetComponent<Pointer>();

		// open file dialog
		fileChooser = (GameObject.Find ("FileDialog")).GetComponent<InputField>();
		fileChooser.Select ();
		fileChooser.ActivateInputField ();
		fileChooser.onValueChange.AddListener (FileDialogUpdate);
		fileChooser.onValidateInput += delegate(string input, int charIndex, char addedChar) { return Validate( addedChar ); };
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
				if(Sphere_Size < 8F){
					Sphere_Size = 8F;
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
			}else if (Input.GetKeyDown(KeyCode.Space)) {
				p.Open();
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
			}else if (Input.GetKeyDown(KeyCode.Keypad1)) {
			Material sky_1 = (Material) Resources.Load("Sunny2 Skybox");
			RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad2)) {
				Material sky_1 = (Material) Resources.Load("sky5x1");
				RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad3)) {
				Material sky_1 = (Material) Resources.Load("sky5x2");
				RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad4)) {
				Material sky_1 = (Material) Resources.Load("sky5x3");
				RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad5)) {
				Material sky_1 = (Material) Resources.Load("sky5x4");
				RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad6)) {
				Material sky_1 = (Material) Resources.Load("sky5x5");
				RenderSettings.skybox = sky_1;
			}else if (Input.GetKeyDown(KeyCode.Keypad0)) {
			RenderSettings.skybox = null;
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

		if (input.Length <= 2)
			return;
		EventSystem curr = EventSystem.current;
		GameObject currFileDialog = curr.currentSelectedGameObject;
		GameObject currCanvas = currFileDialog.transform.parent.gameObject;

		if (input.Substring (0, 2) == "O ") {
			if (!File.Exists (input.Substring(2)))
				return;
			string text = File.ReadAllText (input.Substring(2));
			SetBufText (currCanvas, text);
			currFileDialog.SetActive (false);
		}
		else if (input.Substring (0, 2) == "S ") {
			if ((input.Substring(input.Length-3) == "   ")){
				string filename = input.Substring(2,input.Length-5);
				InputField currInputField = currCanvas.transform.FindChild("InputField").gameObject.GetComponent<InputField>();
				File.WriteAllText(filename, currInputField.text);
				currFileDialog.SetActive (false); 
			}
		}
	}

	private void SetBufText(GameObject buf, string text)
	{
		InputField inputfield = (InputField)(buf.GetComponentsInChildren (typeof(InputField))) [0];
		inputfield.text = text;
	}

	private char Validate(char input)
	{
		Debug.Log (input);
		return input;
	}
		
}