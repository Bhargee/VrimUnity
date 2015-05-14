using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using vrim;

public class Pointer : BaseInputModule {
	
	public const int kLookId = -3;
	public string controlAxisName = "Horizontal";
	private PointerEventData lookData;
	private bool locked = false;
	
	// use screen midpoint as locked pointer location, enabling look location to be the "mouse"
	private PointerEventData GetLookPointerEventData() {
		Vector2 lookPosition;
		lookPosition.x = Screen.width/2;
		lookPosition.y = Screen.height/2;
		if (lookData == null) {
			lookData = new PointerEventData(eventSystem);
		}
		lookData.Reset();
		lookData.delta = Vector2.zero;
		lookData.position = lookPosition;
		lookData.scrollDelta = Vector2.zero;
		eventSystem.RaycastAll(lookData, m_RaycastResultCache);
		lookData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
		m_RaycastResultCache.Clear();
		return lookData;
	}
	
	private bool SendUpdateEventToSelectedObject() {Cloth:
		if (eventSystem.currentSelectedGameObject == null)
			return false;
		BaseEventData data = GetBaseEventData ();
		Buffer buf = (Buffer)eventSystem.currentSelectedGameObject.GetComponent<Buffer> ();
		//if (buf != null)
		//	buf.UpdateState (data);
		ExecuteEvents.Execute (eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
		return data.used;
	}

	public void LockOn()
	{
		GameObject lookedAt = lookData.pointerCurrentRaycast.gameObject;
		if (locked) {
			if (lookedAt == null)
			{
				eventSystem.SetSelectedGameObject(null);
			}
			else 
			{
				selectBuf(lookedAt);
			}
			locked = false;
		} 
		else {
			selectBuf(lookedAt);
			locked = true;
		}
	}	
	public void Open()
	{
		GameObject lookedAt = lookData.pointerCurrentRaycast.gameObject;
		if (lookedAt != null)
		{
			GameObject newPressed = ExecuteEvents.ExecuteHierarchy (lookedAt, lookData, ExecuteEvents.selectHandler);
			if (newPressed != null) {
				GameObject newPressed2 = (GameObject) newPressed.transform.parent.gameObject.transform.FindChild("FileDialog").gameObject;
				if(newPressed2.active){
					newPressed2.SetActive (false);
				}else{
					newPressed2.SetActive (true);
					newPressed2.GetComponent<InputField>().Select();
 
				}
			}
		}
	}

	private void selectBuf(GameObject lookedAt)
	{
		eventSystem.SetSelectedGameObject(null);
		if (lookedAt != null)
		{
			GameObject newPressed = ExecuteEvents.ExecuteHierarchy (lookedAt, lookData, ExecuteEvents.submitHandler);
			if (newPressed != null) {
				eventSystem.SetSelectedGameObject(newPressed);
//				Event fake = Event.KeyboardEvent(KeyCode.RightArrow);
				EventSystem ev = EventSystem.current;

				// TODO change this to buf cursor position
				(newPressed.GetComponent<InputField>()).MoveTextStart(false);
				locked = true;
			}
		}
	
	}
	
	public override void Process() {
		// send update events if there is a selected object - this is important for InputField to receive keyboard events
		SendUpdateEventToSelectedObject();
		PointerEventData lookData = GetLookPointerEventData();
		// use built-in enter/exit highlight handler
		HandlePointerExitAndEnter(lookData,lookData.pointerCurrentRaycast.gameObject);
		/*if (eventSystem.currentSelectedGameObject && controlAxisName != null && controlAxisName != "") {
			float newVal = Input.GetAxis (controlAxisName);
			if (newVal > 0.01f || newVal < -0.01f) {
				AxisEventData axisData = GetAxisEventData(newVal,0.0f,0.0f);
				ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisData, ExecuteEvents.moveHandler);
			}
		}*/
	}   
}