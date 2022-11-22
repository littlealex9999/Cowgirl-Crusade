using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwapRails : SwapRails
{
    public RailsMovement newThingToFollow;

    private void OnTriggerEnter(Collider other)
    {
        RailsEnemyMovement enemyRailsScript = other.GetComponent<RailsEnemyMovement>();
        if (enemyRailsScript != null) {
            if (newThingToFollow != null) {
                enemyRailsScript.thingToFollow = newThingToFollow;
            } else {
                base.OnTriggerEnter(other);
            }
        }
    }
}
