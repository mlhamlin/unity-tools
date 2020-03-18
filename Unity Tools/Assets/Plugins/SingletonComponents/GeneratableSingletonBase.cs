using UnityEngine;

namespace SingletonComponents
{
    public abstract class GeneratableSingletonBase<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static bool _applicationIsQuitting;

        public static bool SafeToAccess => !_applicationIsQuitting;

        public static bool InstanceValueSet => _instance != null;

        public static T Instance => GetInstance();

        public static T GetInstance()
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("Application is quitting it isn't safe to access singleton of type '"
                                 + typeof(T).Name + "'.");
                return null;
            }

            if (InstanceValueSet) return _instance;

            if (!Helpers.TryFindOneInstance(out _instance))
            {
                GenerateNewInstanceOfSingleton();
            }

            return _instance;
        }

        private static void GenerateNewInstanceOfSingleton()
        {
            Debug.Log("Generating component of type " + typeof(T).Name);
            var obj = new GameObject(typeof(T).Name + "[Generated Singleton]");
            _instance = obj.AddComponent<T>();
        }

        protected virtual void OnApplicationQuit()
        {
            _applicationIsQuitting = true;
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Debug.Log("A duplicate singleton component of type " + typeof(T).Name +
                          " woke up and was destroyed. If possible minimize this behavior.");
                Destroy(gameObject);
            }
        }
    }
}