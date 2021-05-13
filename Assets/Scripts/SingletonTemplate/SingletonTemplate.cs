using UnityEngine;

namespace SingletonTemplate
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance 
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError($"Singleton {typeof(T)} was not found!");
                    }
                }

                return _instance;
            }
        }
    }
}