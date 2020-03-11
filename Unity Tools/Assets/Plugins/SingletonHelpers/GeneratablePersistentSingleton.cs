using UnityEngine;

namespace SingletonHelpers
{
    public abstract class GeneratablePersistentSingleton<T> : SingletonBase<T> where T : Component
    {
        public static T Instance => GetOrGenerateInstance();

        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}