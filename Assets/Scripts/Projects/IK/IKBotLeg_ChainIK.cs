using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace StealthMarine.IK
{
    public class IKBotLeg_ChainIK : IKBotLeg
    {
        [SerializeField]
        ChainIKConstraint cIK;
        ChainIKConstraintData cIKData;

        protected override void SetIKData(GameObject targetObj)
        {
            cIKData = cIK.data;
            cIKData.target = targetObj.transform;
            cIKData.tipRotationWeight = 0;
            cIK.data = cIKData;
        }
    }
}