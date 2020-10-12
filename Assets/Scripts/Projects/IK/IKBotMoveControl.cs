using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine {
    public class IKBotMoveControl :BaseControlMono
    {
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float turnSpeed;

        public override void Init()
        {
            
        }

        public override void ControlAcionsFixed()
        {
            float h = inputManager.GetAxisInput("Horizontal");
            float v = inputManager.GetAxisInput("Vertical");

            Quaternion rotate = Quaternion.Euler(0, h * 360 * turnSpeed * Time.deltaTime, 0);
            this.transform.rotation *= rotate;

            this.transform.position += this.transform.forward * v * moveSpeed * Time.deltaTime;
        }

        public override void OnActiveChanged(in bool value)
        {
            
        }
    }
}