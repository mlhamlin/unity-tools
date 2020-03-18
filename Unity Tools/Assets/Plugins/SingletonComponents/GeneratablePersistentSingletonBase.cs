using UnityEngine;

namespace SingletonComponents
{
    public abstract class GeneratablePersistentSingletonBase<T> : GeneratableSingletonBase<T> where T : Component
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}