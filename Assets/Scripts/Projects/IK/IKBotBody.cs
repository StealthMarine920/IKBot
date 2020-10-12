using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace StealthMarine.IK
{
    public enum BodyMoveMode
    {
        byCenter,
        byCheckPoints,
        byLegs
    }
    public class IKBotBody : MonoBehaviour
    {
        [Header("Legs Settings")]
        public Vector3 maxStep;
        public float legMoveSpeed;
        public float stepHeight;
        public AnimationCurve stepCurve;
        public LayerMask rayCastLayer;

        public Transform mainBody;
        public RigBuilder rigBuilder;

        public IKBotLeg[] legs;
        public Transform[] checkPoints;
        RaycastHit hit;

        void OnEnable()
        {
            Init();
        }

        void Init()
        {
            int count = legs.Length;

            for (int i = 0; i < count; i++)
            {
                legs[i].Init(i, rayCastLayer, checkPoints[i]);
            }

            rigBuilder.Build();
        }

        void FixedUpdate()
        {
            for (int i = 0; i < legs.Length; i++)
            {
                legs[i].CheckAndMoveLeg(maxStep, rayCastLayer, legMoveSpeed, stepHeight, stepCurve);
            }

            KeepHeight();
            if(bodyBalance)
                BodyBalance();
        }

        [Header("Body Settings")]
        #region Body Float
        
        public BodyMoveMode bodyMoveMode;
        public float bodyMoveTime;
        Vector3 bodyMoveVelocity;
        [Range(0,10)]public float bodyFloatHeight;

        private void KeepHeight()
        {
            Vector3 newPos;
            switch (bodyMoveMode)
            {
                case BodyMoveMode.byLegs:

                    newPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                    for (int i = 0; i < legs.Length; i++)
                    {
                        newPos.y += legs[i].footIKTarget.position.y;
                    }
                    newPos.y = newPos.y / legs.Length + bodyFloatHeight;

                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newPos, ref bodyMoveVelocity, bodyMoveTime);

                    break;

                case BodyMoveMode.byCenter:

                    Ray ray = new Ray(this.transform.position, -Vector3.up);
                    if (Physics.Raycast(ray, out hit, 100, rayCastLayer))
                    {
                        newPos = new Vector3(this.transform.position.x, bodyFloatHeight + hit.point.y, this.transform.position.z);
                        this.transform.position = Vector3.SmoothDamp(this.transform.position, newPos, ref bodyMoveVelocity, bodyMoveTime);
                    }

                    break;

                case BodyMoveMode.byCheckPoints:

                    newPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);

                    for (int i = 0; i < legs.Length; i++)
                    {
                        Ray ray2 = new Ray(legs[i].targetCheckPoint.position, -Vector3.up);
                        if (Physics.Raycast(ray2, out hit, 100, rayCastLayer))
                        {
                            newPos.y += hit.point.y;
                        }
                    }
                    newPos.y = newPos.y / legs.Length + bodyFloatHeight;

                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newPos, ref bodyMoveVelocity, bodyMoveTime);

                    break;
            }
        }
        #endregion

        #region Body Balance

        public bool bodyBalance;
        public float bodyRotateTime;
        Vector3 rotDir = Vector3.zero;
        Vector3 newRotDir = Vector3.zero;
        Vector3 bodyRotateVelocity;

        private void BodyBalance()
        {
            
            newRotDir = Vector3.zero;

            for (int i = 0; i < legs.Length; i++)
            {
                Ray ray2 = new Ray(legs[i].targetCheckPoint.position, -Vector3.up);
                if (Physics.Raycast(ray2, out hit, 100, rayCastLayer))
                {
                    newRotDir = Vector3.Normalize(newRotDir + hit.normal);
                }
            }

            Debug.DrawRay(this.transform.position, newRotDir, Color.yellow, 0.01f);

            rotDir = Vector3.SmoothDamp(rotDir, Vector3.Normalize(newRotDir), ref bodyRotateVelocity, bodyRotateTime);

            Vector3 binormal = Vector3.Cross(rotDir, this.transform.forward).normalized;

            Vector3 tangent = Vector3.Cross(binormal, rotDir);

            Quaternion rotate = Quaternion.LookRotation(tangent, rotDir);

            mainBody.rotation = rotate;
        }
        #endregion
    }
}