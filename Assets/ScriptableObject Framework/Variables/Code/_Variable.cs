using UnityEngine;

namespace Variable
{
	public abstract class BaseVariable : ScriptableObject
	{
		public abstract void SaveToInitialValue ();
		public abstract void ToggleRuntimePersistance ();
		public abstract void ToggleRuntimeMode ();
	}

	public class GenericVariable<T> : BaseVariable, ISerializationCallbackReceiver 
	{
		public enum RuntimeMode { ReadOnly = 0, ReadWrite = 1 }
		public enum PersistanceMode { None = 0, Persist = 1}
		public T Value
		{
			get => (persistant) ? initialValue : runtimeValue;
			set
			{
				switch (runtimeMode)
				{
					case RuntimeMode.ReadOnly: { Debug.LogWarning($"Attempted to set read only varaible", this); break; }
					case RuntimeMode.ReadWrite:
					{
						if (persistant) { initialValue = value; }
						else { runtimeValue = value; }
						break;
					}
					default: Debug.LogWarning($"Runtime mode switch defaulted", this); break;
				}
			}
		}

		private bool persistant => persistantMode == PersistanceMode.Persist;

		[Header("Value Settings")]

		[SerializeField] private T initialValue;
		[SerializeField] private T runtimeValue;
		[SerializeField] private RuntimeMode runtimeMode;
		[SerializeField] private PersistanceMode persistantMode;

		public static implicit operator T(GenericVariable<T> variable) { return variable.Value; }

		public void OnAfterDeserialize() 
		{ 
			if (!persistant) { runtimeValue = initialValue; } 
		}
		public void OnBeforeSerialize() { }

		public override void SaveToInitialValue ()
		{
			initialValue = runtimeValue;
		}
		public override void ToggleRuntimePersistance ()
		{
			persistantMode = (persistantMode == 0) ? (PersistanceMode) 1 : 0;

		}
		public override void ToggleRuntimeMode ()
		{
			runtimeMode = (runtimeMode == 0) ? (RuntimeMode) 1 : 0;
		}
	}
}