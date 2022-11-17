using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailsEnemyMovement : RailsMovement
{
    public RailsMovement thingToFollow;
    public int numPointsAheads = 10;
    public int maxDeviation = 3;
    [SerializeField] bool ignoreDeviation = false;

    void Start()
    {
        if (thingToFollow != null) {
            pathToFollow = thingToFollow.pathToFollow;
            movingToIndex = thingToFollow.movingToIndex + numPointsAheads;
        }
    }

    override protected void Update()
    {
        if (!ignoreDeviation && movingToIndex > thingToFollow.movingToIndex + numPointsAheads + maxDeviation) {
            movingToIndex = thingToFollow.movingToIndex + numPointsAheads;
        }

        base.Update();
    }
}
