using UnityEngine;
using UnityEngine.Events;

namespace Event
{
	[CreateAssetMenu(fileName = "Event", menuName = "Events/Void Event")]
	public class Void : ScriptableObject
	{
		[Space, SerializeField] private UnityEvent Event;

		public void Invoke() => Event?.Invoke();
		public void Add(UnityAction action) => Event.AddListener(action);
		public void Remove(UnityAction action) => Event.AddListener(action);
	}
}