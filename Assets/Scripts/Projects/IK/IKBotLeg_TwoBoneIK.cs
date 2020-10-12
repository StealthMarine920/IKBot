using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace StealthMarine.IK
{
    public class IKBotLeg_TwoBoneIK : IKBotLeg
    {
        [SerializeField]
        TwoBoneIKConstraint tbIK;
        TwoBoneIKConstraintData tbIKData;

        protected override void SetIKData(GameObject targetObj)
        {
            tbIKData = tbIK.data;
            tbIKData.target = targetObj.transform;
            tbIKData.targetRotationWeight = 0;
            tbIK.data = tbIKData;
        }
    }
}