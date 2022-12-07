using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineDollyCart))]
public class CM_FollowLeader : MonoBehaviour
{
    CinemachineDollyCart myCart;

    public CinemachineDollyCart thisCart
    {
        get {
            if (myCart == null) {
                myCart = GetComponent<CinemachineDollyCart>();
            }

            return myCart;
        }
    }

    public CinemachineDollyCart leader;
    public float targetPointsDifference = 10;
    public float maxDeviation = 3;
    public bool ignoreDeviation = false;

    public float targetSpeed = 20;
    public float slowDownMult = 0.01f;

    void Update()
    {
        if (leader != null && thisCart.m_Path != null) {
            float pointsAheadOfTarget = leader.m_Path.PathLength - 1 - leader.m_Position + (thisCart.m_Position);

            bool tooFarLoop = pointsAheadOfTarget > targetPointsDifference + maxDeviation;

            // if it's moving to a far point, further than deviation allows,
            // or if it's looped to the start and is too far, slow down a lot
            // if it's moving far enough, but within deviation, set speed to leader's speed
            // otherwise, move at target speed
            if (!ignoreDeviation && thisCart.m_Position > leader.m_Position + targetPointsDifference + maxDeviation ||
                !ignoreDeviation && (leader.m_Position + targetPointsDifference + maxDeviation > thisCart.m_Path.PathLength - 1 &&
                tooFarLoop && thisCart.m_Path.Looped)) {
                myCart.m_Speed = Mathf.Lerp(0, targetSpeed, 1 / (pointsAheadOfTarget * slowDownMult));
            } else if (!ignoreDeviation && thisCart.m_Position > leader.m_Position + targetPointsDifference ||
                       !ignoreDeviation && (leader.m_Position + targetPointsDifference > thisCart.m_Path.PathLength - 1 &&
                       pointsAheadOfTarget > targetPointsDifference)) {
                thisCart.m_Speed = leader.m_Speed;
            } else {
                thisCart.m_Speed = targetSpeed;
            }
        }
    }
}
