using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovement : MonoBehaviour
{
    public float newSpeed;
    public float newRotationSpeed;

    public bool playerOnly;
    public bool deleteOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (playerOnly && other.tag != "Player") {
            return;
        }

        RailsMovement rm = other.GetComponent<RailsMovement>();
        if (rm != null) {
            rm.SetSpeed(newSpeed);
            rm.SetRotationSpeed(newRotationSpeed);

            if (deleteOnTrigger) {
                Destroy(gameObject);
            }
        }
    }
}
