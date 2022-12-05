using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwapRails : SwapRails
{
    [Space] public RailsMovement newThingToFollow;

    protected void OnTriggerEnter(Collider other)
    {
        RailsEnemyMovement enemyRailsScript = other.GetComponent<RailsEnemyMovement>();
        EnemyTriggerLogic(enemyRailsScript, other);
    }

    protected void EnemyTriggerLogic(RailsEnemyMovement enemyRailsScript, Collider other)
    {
        if (enemyRailsScript != null) {
            if (newThingToFollow != null) {
                SwapRailsEnemy(enemyRailsScript);
            } else {
                enemyRailsScript.thingToFollow = null;
                foreach (Enemy e in enemyRailsScript.GetComponentsInChildren<Enemy>()) {
                    e.FindTarget();
                    Player.RemoveAttacker(); // you can totally break this and good job if you do
                }

                enemyRailsScript.SetSpeed(enemyRailsScript.targetSpeed);
                SwapRailsLogic(enemyRailsScript);
            }
        }
    }

    protected void SwapRailsEnemy(RailsEnemyMovement scriptToSwap)
    {
        scriptToSwap.thingToFollow = newThingToFollow;
        scriptToSwap.pathToFollow = newThingToFollow.pathToFollow;
        scriptToSwap.movingToIndex = newThingToFollow.movingToIndex + scriptToSwap.numPointsAhead;

        foreach (Enemy e in scriptToSwap.GetComponentsInChildren<Enemy>()) {
            e.FindTarget();
            Player.AddAttacker();
        }
    }
}
