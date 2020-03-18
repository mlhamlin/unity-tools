using System.IO;
using UnityEngine;

namespace SingletonComponents
{
    public abstract class LoadableSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        private static bool _applicationIsQuitting;

        private const string PATH_PREFIX = "Singletons/";

        public static bool IsInstanceValueSet => _instance == null;
        public static T Instance => GetInstance();

        public static T GetInstance()
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("Application is quitting it isn't safe to access singleton of type '"
                                 + typeof(T).Name + "'.");
                return null;
            }

            if (IsInstanceValueSet) return _instance;

            if (!Helpers.TryFindOneInstance(out _instance))
            {
                TryLoadInstanceOfSingleton();
            }

            return _instance;
        }

        private static void TryLoadInstanceOfSingleton()
        {
            Debug.Log("Attempting to load component of type " + typeof(T).Name + " from resources.");
            var path = PATH_PREFIX + typeof(T).Name;
            var instanceObj = Object.Instantiate(Resources.Load(path)) as GameObject;
            if (instanceObj == null)
            {
                Debug.Log("Failed to load prefab from resources.  Expected prefab at location " + path);
                _instance = null;
            }
            else
            {
                instanceObj.name = typeof(T).Name + "[Loaded Singleton]";
                _instance = instanceObj.GetComponent<T>();
            }
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