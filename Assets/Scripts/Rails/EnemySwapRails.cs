using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwapRails : SwapRails
{
    [Space] public RailsMovement newThingToFollow;

    private void OnTriggerEnter(Collider other)
    {
        RailsEnemyMovement enemyRailsScript = other.GetComponent<RailsEnemyMovement>();
        if (enemyRailsScript != null) {
            if (newThingToFollow != null) {
                enemyRailsScript.thingToFollow = newThingToFollow;
                enemyRailsScript.pathToFollow = newThingToFollow.pathToFollow;
                enemyRailsScript.movingToIndex = newThingToFollow.movingToIndex + enemyRailsScript.numPointsAhead;

                foreach (Enemy e in enemyRailsScript.GetComponentsInChildren<Enemy>()) {
                    e.FindTarget();
                }
            } else {
                base.OnTriggerEnter(other);
            }
        }
    }
}
