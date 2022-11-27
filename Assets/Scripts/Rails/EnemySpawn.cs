using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : EnemySwapRails
{
    [SerializeField] List<RailsEnemyMovement> enemyMoveScripts;

    [SerializeField] bool forceOntoPath;

    private void OnTriggerEnter(Collider other)
    {
        foreach (RailsEnemyMovement rem in enemyMoveScripts) {
            rem.gameObject.SetActive(true);
            EnemyTriggerLogic(rem, other);

            rem.transform.position = rem.pathToFollow.getPath[rem.movingToIndex];
        }
    }
}
