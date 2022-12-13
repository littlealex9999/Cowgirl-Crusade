using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject target;
    public bool giveYRotationToParent;

    private void Start()
    {
        if (giveYRotationToParent && transform.parent == null) {
            giveYRotationToParent = false;
        }
    }

    void Update()
    {
        if (target != null) {
            AimAtTarget();
        }
    }


    void AimAtTarget()
    {
        if (giveYRotationToParent) {
            float distance = (target.transform.position - transform.position).magnitude;
            transform.parent.LookAt(new Vector3(target.transform.position.x, transform.parent.position.y, target.transform.position.z));
            transform.LookAt(new Vector3(transform.position.x + transform.parent.forward.x * distance, 
                                         target.transform.position.y, 
                                         transform.position.z + transform.parent.forward.z * distance));
        } else {
            transform.LookAt(target.transform);
        }
    }
}
