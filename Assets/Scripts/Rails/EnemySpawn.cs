using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : EnemySwapRails
{
    [SerializeField] List<RailsEnemyMovement> enemyMoveScripts;

    [SerializeField] bool forceOntoPath;
    [SerializeField] bool setRotation;

    [SerializeField] bool conditionalSpawn;
    [SerializeField] int maxEnemyRequirement;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (conditionalSpawn && Player.GetNumAttackers() > maxEnemyRequirement) {
                return;
            }

            foreach (RailsEnemyMovement rem in enemyMoveScripts) {
                rem.gameObject.SetActive(true);
                EnemyTriggerLogic(rem, other);

                if (forceOntoPath) {
                    rem.transform.position = rem.pathToFollow.getPath[rem.movingToIndex];
                }
                if (setRotation) {
                    if (rem.thingToFollow != null) {
                        rem.transform.rotation = rem.thingToFollow.transform.rotation;
                    }
                }
            }
        }
    }
}
