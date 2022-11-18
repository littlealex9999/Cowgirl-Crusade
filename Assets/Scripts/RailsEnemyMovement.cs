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
    public float slowDownMult = 0.19f;

    void Start()
    {
        if (thingToFollow != null) {
            pathToFollow = thingToFollow.pathToFollow;
            movingToIndex = thingToFollow.movingToIndex + numPointsAhead;
        }
    }

    override protected void Update()
    {
        int pointsAheadOfTarget =
            pathToFollow.getPath.Length - 1 - thingToFollow.movingToIndex +
            (movingToIndex);

        // if it's moving to a far point, further than deviation allows,
        // or if it's looped to the start and is too far
        bool tooFarLoop = pointsAheadOfTarget > numPointsAhead + maxDeviation;

        if (!ignoreDeviation && movingToIndex > thingToFollow.movingToIndex + numPointsAhead + maxDeviation || 
            !ignoreDeviation && (thingToFollow.movingToIndex + numPointsAhead + maxDeviation > pathToFollow.getPath.Length - 1 && 
            tooFarLoop)) {
            speed = targetSpeed / (slowDownMult * pointsAheadOfTarget);
        } else {
            speed = targetSpeed;
        }

        base.Update();
    }
}
