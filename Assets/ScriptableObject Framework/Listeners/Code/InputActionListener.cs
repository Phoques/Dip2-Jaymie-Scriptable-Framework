using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Project
{
	public class InputActionListener : MonoBehaviour
	{
		public Event.InputAction @event;
		[Space] 
		public UnityEvent<InputAction.CallbackContext> unityEvent;

		private void OnEnable ()
		{
			@event.Add(unityEvent.Invoke);
		}
		private void OnDisable ()
		{
			@event.Remove(unityEvent.Invoke);
		}
	}
}