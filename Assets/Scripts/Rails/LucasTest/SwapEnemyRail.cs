using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEnemyRail : MonoBehaviour
{
    public BezierPath newPath;
    public int newMovePoint;

    public bool destroyOnEnemyEnter;


    protected void OnTriggerEnter(Collider other)
    {
        RailsMovement moveScript = other.GetComponent<RailsMovement>();
        if (moveScript != null && newPath != null) {
            moveScript.pathToFollow = newPath;
            moveScript.movingToIndex = newMovePoint;

            if (destroyOnEnemyEnter && other.tag == "Enemy") {
                Destroy(gameObject);
            }
        }
    }
}
