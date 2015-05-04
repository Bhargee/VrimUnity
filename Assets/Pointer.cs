using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

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
	
	private bool SendUpdateEventToSelectedObject() {
		if (eventSystem.currentSelectedGameObject == null)
			return false;
		BaseEventData data = GetBaseEventData ();
		ExecuteEvents.Execute (eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
		return data.used;
	}

	public void LockOn()
	{
		GameObject lookedAt = lookData.pointerCurrentRaycast.gameObject;
		GameObject currBuf = eventSystem.currentSelectedGameObject != null ? eventSystem.currentSelectedGameObject.gameObject: null;
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

	private void selectBuf(GameObject lookedAt)
	{
		eventSystem.SetSelectedGameObject(null);
		if (lookedAt != null)
		{
			GameObject newPressed = ExecuteEvents.ExecuteHierarchy (lookedAt, lookData, ExecuteEvents.submitHandler);
			if (newPressed != null) {
				eventSystem.SetSelectedGameObject(newPressed);
				(newPressed.GetComponent<InputField>()).MoveTextEnd(false);
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
		bool commandKeysPressed = Input.GetKey (KeyCode.L) && (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl));
		if (Input.GetKeyDown(KeyCode.B)) {
			eventSystem.SetSelectedGameObject(null);
			if (lookData.pointerCurrentRaycast.gameObject != null) {
				GameObject go = lookData.pointerCurrentRaycast.gameObject;
				GameObject newPressed = ExecuteEvents.ExecuteHierarchy (go, lookData, ExecuteEvents.submitHandler);
				if (newPressed != null) {
					eventSystem.SetSelectedGameObject(newPressed);
					(newPressed.GetComponent<InputField>()).MoveTextEnd(false);
				}
			}
		}
		if (eventSystem.currentSelectedGameObject && controlAxisName != null && controlAxisName != "") {
			float newVal = Input.GetAxis (controlAxisName);
			if (newVal > 0.01f || newVal < -0.01f) {
				AxisEventData axisData = GetAxisEventData(newVal,0.0f,0.0f);
				ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisData, ExecuteEvents.moveHandler);
			}
		}
	}   
}