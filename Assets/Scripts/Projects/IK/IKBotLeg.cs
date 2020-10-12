using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace StealthMarine.IK
{

    public class IKBotLeg : MonoBehaviour
    {
        [ReadOnly]
        public bool footMoving;
        [ReadOnly]
        public Transform footIKTarget;
        [ReadOnly]
        public Transform targetCheckPoint;
        public IKBotLeg[] oppositeLegs;

        [HideInInspector]
        public Vector3 oldCheckPoint;
        [HideInInspector]
        public Vector3 oldTargetPoint;
        [HideInInspector]
        public Vector3 newTargetPoint;

        public void Init(int index, LayerMask rayCastLayer, Transform checkPoint)
        {
            GameObject targetObj = new GameObject("Target " + index);
            footIKTarget = targetObj.transform;

            SetIKData(targetObj);

            targetCheckPoint = checkPoint;

            oldCheckPoint = targetCheckPoint.position;
            RaycastHit hit;
            Ray ray = new Ray(targetCheckPoint.position, -Vector3.up);
            if (Physics.Raycast(ray, out hit, 100, rayCastLayer))
            {
                footIKTarget.position = hit.point;
            }

            oldTargetPoint = footIKTarget.position;
        }

        protected virtual void SetIKData(GameObject targetObj) {}

        public void CheckAndMoveLeg(Vector3 maxStep, LayerMask rayCastLayer, float legMoveSpeed, float stepHeight, AnimationCurve stepCurve)
        {
            if (Vector3.Distance(oldCheckPoint, targetCheckPoint.position) > maxStep.x && !footMoving)
            {
                bool canMove = true;
                foreach(IKBotLeg oppositeLeg in oppositeLegs)
                {
                    if (oppositeLeg.footMoving)
                        canMove = false;
                }

                if (canMove)
                {
                    footMoving = true;
                    Vector3 newCheckPoint = targetCheckPoint.position + (targetCheckPoint.position - oldCheckPoint) * 1.5f;

                    RaycastHit hit;
                    Ray ray = new Ray(newCheckPoint, -Vector3.up);

                    if (Physics.Raycast(ray, out hit, 100, rayCastLayer))
                    {
                        newTargetPoint = hit.point;
                    }
                }
            }

            if (footMoving)
                MoveLegIK(legMoveSpeed, stepHeight, stepCurve);
        }

        float moveVelocity = 0.0f;
        public void MoveLegIK( float legMoveSpeed, float stepHeight, AnimationCurve stepCurve)
        {
            if (Vector3.Distance(footIKTarget.position, newTargetPoint) > 0.01f)
            {
                moveVelocity += legMoveSpeed * Time.deltaTime;
                moveVelocity = Mathf.Clamp(moveVelocity, 0, 1);

                footIKTarget.position = Vector3.Lerp(oldTargetPoint, newTargetPoint, moveVelocity);

                float step = stepHeight * stepCurve.Evaluate(moveVelocity);
                footIKTarget.position += new Vector3(0, step, 0);
            }
            else
            {
                oldCheckPoint = targetCheckPoint.position;
                oldTargetPoint = footIKTarget.position;
                moveVelocity = 0.0f;
                footMoving = false;
            }
        }

        
    }
}