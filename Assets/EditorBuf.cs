using System;
using UnityEngine;
using UnityEngine.UI;
namespace vrim
{
		public class EditorBuf : InputField
		{
				public EditorBuf () : base()
				{
					Debug.Log ("Calling editorbuf constructor");
					Debug.Log (this.m_CaretPosition);
				}
		}
}

