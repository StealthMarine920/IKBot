using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine 
{
    public class CameraBracket : BaseControlMono
    {
        public Transform followTarget;
        public float followTimeCost;
        public float rotateSpeed;
        private Vector3 followVelocity;

        public override void Init()
        {

        }

        public override void ControlAcionsFixed()
        {
            float mouseX = inputManager.GetAxisInput("Mouse X");
            float mouseY = inputManager.GetAxisInput("Mouse Y");
            //摄像机架跟随
            this.transform.position = Vector3.SmoothDamp(this.transform.position, followTarget.position, ref followVelocity, followTimeCost);

            if (mouseX != 0)
            {
                this.transform.Rotate(0, mouseX * rotateSpeed * Time.deltaTime, 0, Space.World);

                if (this.transform.eulerAngles.x < 300 && this.transform.eulerAngles.x >= 180)
                {
                    this.transform.eulerAngles = new Vector3(300, this.transform.eulerAngles.y, 0);
                }
                else if (this.transform.eulerAngles.x > 40 && this.transform.eulerAngles.x < 180)
                {
                    this.transform.eulerAngles = new Vector3(40, this.transform.eulerAngles.y, 0);
                }
            }

            if (mouseY != 0)
            {
                this.transform.Rotate(-mouseY * rotateSpeed * Time.deltaTime, 0, 0, Space.Self);
            }

            //mCam.transform.eulerAngles = new Vector3(mCam.transform.eulerAngles.x, mCam.transform.eulerAngles.y, 0);
        }
    }
}