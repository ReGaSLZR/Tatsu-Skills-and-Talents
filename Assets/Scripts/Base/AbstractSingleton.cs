namespace ReGaSLZR
{

    using UnityEngine;

    public abstract class AbstractSingleton<T> : MonoBehaviour
        where T : MonoBehaviour
    {

        #region Singleton Instance

        private static readonly object lockObject = new object();
        private static T instance;
        public static T Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                    }

                    return instance;
                }
            }
        }

        #endregion

        [SerializeField]
        private bool isDontDestroyOnLoad;

        protected virtual void Awake()
        {
            InitSingleton();
        }

        #region Class Implementation

        private void InitSingleton()
        {
            if (Instance.GetInstanceID() == GetInstanceID())
            {
                Debug.Log($"{gameObject.name}.{GetType().Name}.Awake(): " +
                    $"Accepted as singleton.");

                if (isDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}.{GetType().Name}.Awake(): " +
                    $"Cannot have >1 Instances of this class. Destroying...");
                Destroy(gameObject);
            }
        }

        #endregion

    }

}