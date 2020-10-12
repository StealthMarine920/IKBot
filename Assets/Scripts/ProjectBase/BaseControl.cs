using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StealthMarine.Manager;

namespace StealthMarine
{
    public class BaseControl
    {
        protected InputManager inputManager;
        public bool active = true;

        public BaseControl()
        {
            inputManager = InputManager.Instance;

            inputManager.Register(this);

            Init();
        }

        virtual public void Init() { }
        virtual public void ControlAcions() { }
        virtual public void ControlAcionsFixed() { }
        virtual public void ControlAcionsLate() { }
    }
}

