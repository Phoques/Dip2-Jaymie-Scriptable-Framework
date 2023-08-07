using UnityEngine;
using UnityEngine.Events;

namespace Project
{
	public class VoidListener : MonoBehaviour
	{
		public Event.Void @event;
		[Space] 
		public UnityEvent unityEvent;

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