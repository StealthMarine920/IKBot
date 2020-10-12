using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine
{
    public class BotAutoMove : MonoBehaviour
    {
        [SerializeField]
        private float turnAngle;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float turnSpeed;
        [SerializeField]
        private CharacterController cc;

        private void Start()
        {
            cc = this.GetComponent<CharacterController>();
        }

        void Update()
        {
            Quaternion rotate = Quaternion.Euler(0, turnAngle * turnSpeed * Time.deltaTime, 0);
            this.transform.rotation *= rotate;
            if(cc != null)
            {
                cc.Move(this.transform.forward * moveSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
}