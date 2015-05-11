using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

namespace vrim
{

	public class Buffer : MonoBehaviour
	{
		private string filename;
		private InputField input;


		public void Init(GameObject parentCanvas, string filename)
		{
			this.filename = filename;
			this.input = (InputField) (parentCanvas.GetComponentsInChildren(typeof(InputField)))[0];
		}
	}

}

