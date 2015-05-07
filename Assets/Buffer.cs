using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

namespace vrim
{

	public class Buffer : MonoBehaviour
	{
		private string filename;
		private int cursor = 0;
		private GameObject parentCanvas;


		public void Init(GameObject parentCanvas, string filename)
		{
			this.filename = filename;
			this.parentCanvas = parentCanvas;
			InputField ipf = (InputField) (parentCanvas.GetComponentsInChildren(typeof(InputField)))[0];
			ipf.onValueChange.AddListener (ValueChange);
		}
		
		public void ValueChange(string input)
		{
			Debug.Log (input);
		}

		public void Insert(string toInsert)
		{
		}

		public void Delete(int length)
		{
		}

	}

}

