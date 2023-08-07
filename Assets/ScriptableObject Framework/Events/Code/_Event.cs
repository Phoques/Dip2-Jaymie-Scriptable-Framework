using UnityEngine;
using UnityEngine.Events;

namespace Event
{
	public interface IEvent { }

	public class GenericEvent<T> : ScriptableObject
	{
		[Space, SerializeField] private UnityEvent<T> EventReponses;

		public void Invoke (T context) => EventReponses?.Invoke(context);
		public void Add(UnityAction<T> action) => EventReponses.AddListener(action);
		public void Remove(UnityAction<T> action) => EventReponses.RemoveListener(action);
	}
}
