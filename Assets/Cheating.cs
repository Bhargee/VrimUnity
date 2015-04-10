using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cheating : MonoBehaviour {

	Text txt;
	string myEmancipation = "The man in black fled across the desert, and the gunslinger followed. The desert was the apotheosis of all deserts, huge, standing to the sky for what looked like eternity in all directions. It was white and blinding and waterless and without feature save for the faint, cloudy haze of the mountains which sketched themselves on the horizon and the devil-grass which brought sweet dreams, nightmares, death. An occasional tombstone sign pointed the way, for once the drifted track that cut its way through the thick crust of alkali had been a highway. Coaches and buckas had followed it. The world had moved on since then. The world had emptied.";
	int max_length = 300;
	int max_delete = 14;
	int x = 0;
	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>(); 
		StartCoroutine(GreenYellowRed ());
	}
	IEnumerator GreenYellowRed()
	{
		while (x < max_length) {
			txt.text = myEmancipation.Substring(0,x);
			yield return new WaitForSeconds(0.2f);
			x = x + 1;
		}

		while (x > max_length-20) {
			txt.text = myEmancipation.Substring(0,x);
			yield return new WaitForSeconds(0.2f);
			x = x - 1;
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}