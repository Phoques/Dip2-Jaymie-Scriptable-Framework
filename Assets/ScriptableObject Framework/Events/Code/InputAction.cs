using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Event
{
	[CreateAssetMenu(fileName = "Event", menuName = "Events/Input Action Event")]
	public class InputAction : ScriptableObject
	{	
		[SerializeField, Tooltip("")] 
		private InputActionReference input;

		[Space, SerializeField, Tooltip("")] 
		private UnityEvent<CallbackContext> Actions;

		public int Test { get; private set; } = 0;

		private void Awake()
		{
			hideFlags = HideFlags.DontUnloadUnusedAsset;
		}
		private void OnEnable()
		{
			hideFlags = HideFlags.DontUnloadUnusedAsset;
		
			if (!input) return;
			input.action.performed += Actions.Invoke;
		}
		private void OnDisable()
		{
			if (!input) return;
			input.action.performed -= Actions.Invoke;
		}

		public void Invoke(CallbackContext context) => Actions.Invoke(context);
		public void Add(UnityAction<CallbackContext> action) => Actions.AddListener(action);
		public void Remove(UnityAction<CallbackContext> action) => Actions.RemoveListener(action);

		[RuntimeInitializeOnLoadMethod]
		private static void Load() => Resources.LoadAll("", typeof(InputAction));
	}
}