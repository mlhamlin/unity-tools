using UnityEngine;

namespace SingletonHelpers
{
    public abstract class InSceneSingleton<T> : SingletonBase<T> where T : Component
    {
        public static T Instance => GetInstanceInScene();
    }
}