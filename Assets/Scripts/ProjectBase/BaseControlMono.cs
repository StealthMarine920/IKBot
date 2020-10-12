using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthMarine.Manager;

namespace StealthMarine
{
    public class BaseControlMono : MonoBehaviour
    {
        protected InputManager inputManager;
        private bool active = true;
        public bool isActive
        {
            get 
            { 
                return active; 
            }
            set
            {
                active = value; 
                OnActiveChanged(in value);
            }
        }

        void Start()
        {
            inputManager = InputManager.Instance;

            inputManager.Register(this);

            Init();
        }

        virtual public void Init() { }
        virtual public void ControlAcions() { }
        virtual public void ControlAcionsFixed() { }
        virtual public void ControlAcionsLate() { }
        virtual public void OnActiveChanged(in bool value) { }
    }
}

