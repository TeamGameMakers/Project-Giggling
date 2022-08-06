using UnityEngine;

namespace Base
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        public static T Instance {
            get {
                if (m_instance == null) {
                    GameObject go = new GameObject(typeof(T).Name);
                    m_instance = go.AddComponent<T>();
                }
                return m_instance;
            }
        }

        public bool dontDestroy = true;

        protected virtual void Awake()
        {
            if (m_instance == null) {
                m_instance = this as T;
            }
            else {
                // 直接销毁，会导致 m_instance 置空
                Destroy(gameObject);
            }
        }

        protected virtual void Start()
        {
            if (dontDestroy) {
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            // 要检查是否不销毁，不销毁时不清空，不然在销毁重复的同时会置空单例
            if (!dontDestroy)
                m_instance = null;
        }
    }
}
