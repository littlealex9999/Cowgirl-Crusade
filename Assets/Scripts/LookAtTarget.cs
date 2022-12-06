using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            AimAtTarget();
        }
    }


    void AimAtTarget()
    {
        transform.LookAt(target.transform);
    }


}
