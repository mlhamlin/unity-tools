using UnityEngine;

namespace SingletonComponents
{
    public static class Helpers
    {
        public static bool TryFindOneInstance<T>(out T instance) where T : Component
        {
            var objs = Object.FindObjectsOfType<T>();

            if (objs.Length > 1)
            {
                Debug.LogWarning("There is more than one " + typeof(T).Name + " in the scene.  " +
                                 "Unexpected behavior could result.");
            }

            if (objs.Length > 0)
            {
                instance = objs[0];
                return true;
            }

            instance = null;
            return false;
        }
    }
}