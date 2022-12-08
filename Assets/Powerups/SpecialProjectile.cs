using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{
    public float cooldownInduced;
    public float destroyAfter;

    void Start()
    {
        if (destroyAfter != 0) {
            Destroy(gameObject, destroyAfter);
        }
    }
}
