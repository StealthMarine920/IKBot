using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine.Manager
{
    public class SingleTonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = GameManager.Instance.GetGameObject().AddComponent<T>();
                }
                return m_Instance;
            }
        }
    }
}