using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapRails : MonoBehaviour
{
    public BezierPath newPath;
    public int newMovePoint;

    public bool destroyOnPlayerEnter;


    protected void OnTriggerEnter(Collider other)
    {
        RailsMovement moveScript = other.GetComponent<RailsMovement>();
        if (moveScript != null && newPath != null) {
            moveScript.pathToFollow = newPath;
            moveScript.movingToIndex = newMovePoint;

            if (destroyOnPlayerEnter && other.tag == "Player") {
                Destroy(gameObject);
            }
        }
    }
}
