using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine.Manager
{
    public class InputManager : SingleTonMono<InputManager>
    {
        public bool allowInput = true;
        private List<BaseControl> controlList = new List<BaseControl>();
        private List<BaseControlMono> monoControlList = new List<BaseControlMono>();

        private void FixedUpdate()
        {
            foreach (BaseControl bc in controlList)
            {
                if (bc.active)
                    bc.ControlAcionsFixed();
            }

            foreach (BaseControlMono bcm in monoControlList)
            {
                if (bcm.isActive)
                    bcm.ControlAcionsFixed();
            }
        }

        void Update()
        {
            foreach (BaseControl bc in controlList)
            {
                if(bc.active)
                    bc.ControlAcions();
            }

            foreach (BaseControlMono bcm in monoControlList)
            {
                if (bcm.isActive)
                    bcm.ControlAcions();
            }
        }

        private void LateUpdate()
        {
            foreach (BaseControl bc in controlList)
            {
                if (bc.active)
                    bc.ControlAcionsLate();
            }

            foreach (BaseControlMono bcm in monoControlList)
            {
                if (bcm.isActive)
                    bcm.ControlAcionsLate();
            }
        }

        public void Register(BaseControl bc)
        {
            controlList.Add(bc);
        }

        public void Register(BaseControlMono bcm)
        {
            monoControlList.Add(bcm);
        }

        #region Key Input
        public enum KeyInputType
        {
            Press,
            Down,
            Up
        }
        public bool allowKeyInput = true;

        public bool GetKeyInput(KeyCode key, KeyInputType type)
        {
            if (!allowKeyInput || !allowInput)
                return false;

            switch (type)
            {
                case KeyInputType.Press:
                    return Input.GetKey(key);
                case KeyInputType.Down:
                    return Input.GetKeyDown(key);
                case KeyInputType.Up:
                    return Input.GetKeyUp(key);
                default:
                    return false;
            }
        }
        #endregion

        #region Mouse Input
        public enum MouseInputType
        {
            Click,
            Down,
            Up
        }
        public bool allowMouseInput = true;

        public bool GetMouseInput(int key, MouseInputType type)
        {
            if (!allowMouseInput || !allowInput)
                return false;

            switch (type)
            {
                case MouseInputType.Click:
                    return Input.GetMouseButton(key);
                case MouseInputType.Down:
                    return Input.GetMouseButtonDown(key);
                case MouseInputType.Up:
                    return Input.GetMouseButtonUp(key);
                default:
                    return false;
            }
        }

        public float GetMouseScroll()
        {
            if (!allowMouseInput)
                return 0;

            return Input.GetAxis("Mouse ScrollWheel");
        }

        public bool GetMouseInputRaycast(int key, MouseInputType type,out RaycastHit raycastHit)
        {
            bool input = GetMouseInput(key, type);
            if (input)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                return Physics.Raycast(ray, out raycastHit);
            }
            else
            {
                raycastHit = default;
                return false;
            }
        }
        #endregion

        public float GetAxisInput(string key)
        {
            if (!allowInput)
                return 0;

            return Input.GetAxis(key);
        }

        public float GetAxisRawInput(string key)
        {
            if (!allowInput)
                return 0;

            return Input.GetAxisRaw(key);
        }
    }
}