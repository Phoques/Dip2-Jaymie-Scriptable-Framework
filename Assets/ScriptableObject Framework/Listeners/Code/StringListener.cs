using UnityEngine;
using UnityEngine.Events;

namespace Project
{
	public class StringListener : MonoBehaviour
	{
		public Event.String @event;
		[Space] 
		public UnityEvent<string> unityEvent;

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