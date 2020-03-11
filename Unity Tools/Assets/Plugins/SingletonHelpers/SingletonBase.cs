using UnityEngine;

namespace SingletonHelpers
{
    public abstract class SingletonBase<T> : MonoBehaviour where T : Component
    {
        // ReSharper disable once StaticMemberInGenericType
        private static bool _applicationIsQuitting;
        private static T _instance;

        //This doesn't try to find instances that haven't been hooked up
        // this should mostly be used to safely check if an instance you have been interacting with has been destroyed
        public static bool IsInstanceValueSet => _instance == null;

        protected static T GetInstanceInScene()
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("Application is quitting it isn't safe to access singleton of type '"
                                 + typeof(T).Name + "'.");
                return null;
            }

            if (_instance == null) return _instance;

            TryFindInstance();

            if (_instance == null)
            {
                Debug.LogError("Failed to find component of type " + typeof(T).Name + " in scene.  " +
                               "Please make sure to set up a component of this type in the scene.");
            }

            return _instance;
        }

        protected static T GetOrGenerateInstance()
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("Application is quitting it isn't safe to access singleton of type '"
                                 + typeof(T).Name + "'.");
                return null;
            }

            if (!_instance)
            {
                TryFindInstance();

                if (_instance == null)
                {
                    GenerateNewInstanceOfSingleton();
                }
            }

            return _instance;
        }

        protected static T GetOrLoadInstance()
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("Application is quitting it isn't safe to access singleton of type '"
                                 + typeof(T).Name + "'.");
                return null;
            }

            if (!_instance)
            {
                TryFindInstance();

                if (_instance == null)
                {
                    LoadInstanceOfSingleton();
                }
            }

            return _instance;
        }

        private static void TryFindInstance()
        {
            var objs = FindObjectsOfType<T>();

            if (objs.Length == 1)
            {
                _instance = objs[0];
            }
            else if (objs.Length > 1)
            {
                _instance = objs[0];
                Debug.LogWarning("There is more than one " + typeof(T).Name + " in the scene.  " +
                                 "Unexpected behavior could result.");
            }
        }

        private static void GenerateNewInstanceOfSingleton()
        {
            Debug.Log("Generating component of type " + typeof(T).Name);
            var obj = new GameObject(typeof(T).Name + "[Generated Singleton]");
            _instance = obj.AddComponent<T>();
        }

        private static void LoadInstanceOfSingleton()
        {
            Debug.Log("Loading component of type " + typeof(T).Name);
            var instance = Instantiate(Resources.Load("UnitySingletons/" + typeof(T).Name)) as GameObject;
            if (instance == null) return;
            instance.name = typeof(T).Name + "[Loaded Singleton]";
            _instance = instance.GetComponent<T>();
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