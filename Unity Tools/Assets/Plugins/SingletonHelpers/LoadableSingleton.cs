using UnityEngine;

namespace SingletonHelpers
{
    public abstract class LoadableSingleton<T> : SingletonBase<T> where T : Component
    {
        public static T Instance => GetOrLoadInstance();
    }
}