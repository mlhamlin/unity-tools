using UnityEngine;

namespace SingletonHelpers
{
    public abstract class LoadablePersistentSingleton<T> : SingletonBase<T> where T : Component
    {
        public static T Instance => GetOrLoadInstance();

        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}