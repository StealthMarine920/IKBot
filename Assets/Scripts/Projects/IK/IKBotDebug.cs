using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthMarine.IK
{
    [RequireComponent(typeof(IKBotBody))]
    public class IKBotDebug : MonoBehaviour
    {
        [SerializeField]
        IKBotBody ikbc;
        [SerializeField]
        bool drawDebug;

        private void OnDrawGizmos()
        {
            if (drawDebug)
            {
                for(int i=0;i< ikbc.legs.Length; i++)
                {
                    if (ikbc.legs[i].footMoving)
                    {
                        Gizmos.color = Color.green;
                    }
                    else
                    {
                        Gizmos.color = Color.red;
                    }
                    Gizmos.DrawCube(ikbc.checkPoints[i].position, new Vector3(0.2f,0.2f,0.2f));
                    
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(ikbc.legs[i].oldCheckPoint, 0.1f);
                    if (ikbc.legs[i].footIKTarget != null)
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(ikbc.legs[i].footIKTarget.position, 0.15f);
                        Gizmos.DrawLine(ikbc.checkPoints[i].position, ikbc.legs[i].footIKTarget.position);
                    }

                }
            }
        }
    }
}