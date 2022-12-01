using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCharacters : MonoBehaviour
{
    public float newSpeed;

    private void OnTriggerEnter(Collider other)
    {
        RailsMovement rm = other.GetComponent<RailsMovement>();
        if (rm != null) {
            rm.SetSpeed(newSpeed);
        }
    }
}
