using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine.Manager
{
    public class GameManager
    {
        private static GameManager m_Instance;
        private GameObject gameObject;
        public static GameManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new GameManager();
                    m_Instance.gameObject = new GameObject("_MyGameManager");
                }
                return m_Instance;
            }
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        } 
    }
}