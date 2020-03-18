using UnityEngine;

namespace SingletonComponents
{
    public abstract class LoadablePersistentSingletonBase<T> : LoadableSingleton<T> where T : Component
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}