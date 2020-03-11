using UnityEngine;

namespace SingletonHelpers
{
    public abstract class GeneratableSingleton<T> : SingletonBase<T> where T : Component
    {
        public static T Instance => GetOrGenerateInstance();
    }
}