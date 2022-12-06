using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineDollyCart))]
public class CM_FollowLeader : MonoBehaviour
{
    CinemachineDollyCart myCart;

    public CinemachineDollyCart leader;
    public float targetPointsDifference = 10;
    public float maxDeviation = 3;
    public bool ignoreDeviation = false;

    public float targetSpeed = 20;
    public float slowDownMult = 0.01f;

    private void Start()
    {
        myCart = GetComponent<CinemachineDollyCart>();
    }

    void Update()
    {
        if (leader != null && myCart.m_Path != null) {
            float pointsAheadOfTarget = leader.m_Path.PathLength - 1 - leader.m_Position + (myCart.m_Position);

            bool tooFarLoop = pointsAheadOfTarget > targetPointsDifference + maxDeviation;

            // if it's moving to a far point, further than deviation allows,
            // or if it's looped to the start and is too far, slow down a lot
            // if it's moving far enough, but within deviation, set speed to leader's speed
            // otherwise, move at target speed
            if (!ignoreDeviation && myCart.m_Position > leader.m_Position + targetPointsDifference + maxDeviation ||
                !ignoreDeviation && (leader.m_Position + targetPointsDifference + maxDeviation > myCart.m_Path.PathLength - 1 &&
                tooFarLoop && myCart.m_Path.Looped)) {
                myCart.m_Speed = Mathf.Lerp(0, targetSpeed, 1 / (pointsAheadOfTarget * slowDownMult));
            } else if (!ignoreDeviation && myCart.m_Position > leader.m_Position + targetPointsDifference ||
                       !ignoreDeviation && (leader.m_Position + targetPointsDifference > myCart.m_Path.PathLength - 1 &&
                       pointsAheadOfTarget > targetPointsDifference)) {
                myCart.m_Speed = leader.m_Speed;
            } else {
                myCart.m_Speed = targetSpeed;
            }
        }
    }
}
