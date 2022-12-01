using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsEnemyMovement : RailsMovement
{
    public RailsMovement thingToFollow;
    public int numPointsAhead = 10;
    public int maxDeviation = 3;
    [SerializeField] bool ignoreDeviation = false;

    public float targetSpeed = 5f;
    public float slowDownMult = 0.01f;

    void Start()
    {
        if (thingToFollow != null) {
            pathToFollow = thingToFollow.pathToFollow;
            movingToIndex = thingToFollow.movingToIndex + numPointsAhead;
        } else {
            targetSpeed = speed;
        }
    }

    override protected void Update()
    {
        if (thingToFollow != null) {
            int pointsAheadOfTarget = pathToFollow.getPath.Length - 1 - thingToFollow.movingToIndex + (movingToIndex);

            bool tooFarLoop = pointsAheadOfTarget > numPointsAhead + maxDeviation;

            // if it's moving to a far point, further than deviation allows,
            // or if it's looped to the start and is too far, slow down a lot
            // if it's moving far enough, but within deviation, set speed to leader's speed
            // otherwise, move at target speed
            if (!ignoreDeviation && movingToIndex > thingToFollow.movingToIndex + numPointsAhead + maxDeviation ||
                !ignoreDeviation && (thingToFollow.movingToIndex + numPointsAhead + maxDeviation > pathToFollow.getPath.Length - 1 &&
                tooFarLoop)) {
                speed = Mathf.Lerp(0, targetSpeed, 1 / (pointsAheadOfTarget * slowDownMult));
            } else if (!ignoreDeviation && movingToIndex > thingToFollow.movingToIndex + numPointsAhead ||
                       !ignoreDeviation && (thingToFollow.movingToIndex + numPointsAhead > pathToFollow.getPath.Length - 1 &&
                       pointsAheadOfTarget > numPointsAhead)) {
                speed = thingToFollow.speed;
            } else {
                speed = targetSpeed;
            }
        }

        base.Update();
    }

    public override void SetSpeed(float value)
    {
        targetSpeed = value;
    }
}
