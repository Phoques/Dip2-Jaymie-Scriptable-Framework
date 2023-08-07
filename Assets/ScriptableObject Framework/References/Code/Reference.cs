using System;
using UnityEngine;

namespace Reference 
{
    using Variable;

    [Serializable] public class Bool : Reference<bool, Variable.Bool> { }
    [Serializable] public class Char : Reference<char, Variable.Char> { }
    [Serializable] public class Float : Reference<float, Variable.Float> { }
    [Serializable] public class Int : Reference<int, Variable.Int> { }
    [Serializable] public class Int64 : Reference<long, Variable.Int64> { }
    [Serializable] public class String : Reference<string, Variable.String> { }

    [Serializable] public class AnimationCurve : Reference<UnityEngine.AnimationCurve, Variable.AnimationCurve> { }
    [Serializable] public class GameObject : Reference<UnityEngine.GameObject, Variable.GameObject> { }
    [Serializable] public class Transform : Reference<UnityEngine.Transform, Variable.Transform> { }
    [Serializable] public class Vector2 : Reference<UnityEngine.Vector2, Variable.Vector2> { }
    [Serializable] public class Vector3 : Reference<UnityEngine.Vector3, Variable.Vector3> { }
    [Serializable] public class Sprite : Reference<UnityEngine.Sprite, Variable.Sprite> { }

    public class Reference<T1, T2>
	{
        [SerializeField] public bool useConstant = true;
        [SerializeField] private T1 constantValue;
        [SerializeField] private T2 variable;

        public Reference() { }

        public Reference(T1 value)
        {
            useConstant = true;
            constantValue = value;
        }

        public T1 Value
        {
            get => useConstant ? constantValue : variable as GenericVariable<T1>;
            set
            {
                if (useConstant) constantValue = value;
                else (variable as GenericVariable<T1>).Value = value;
            }
        }

        public static implicit operator T1(Reference<T1, T2> reference) => reference.Value;
    }
}