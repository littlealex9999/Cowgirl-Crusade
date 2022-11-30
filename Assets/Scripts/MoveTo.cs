using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Vector3 moveTo;
    public float speed;

    public bool localPosition;

    [HideInInspector] public bool finished = false;

    void Update()
    {
        if (!finished) {
            if (localPosition) {
                Vector3 moveDist = (moveTo - transform.localPosition).normalized * speed * Time.deltaTime;
                if (moveDist.sqrMagnitude < (moveTo - transform.localPosition).sqrMagnitude) {
                    transform.localPosition += (moveTo - transform.localPosition).normalized * speed * Time.deltaTime;
                } else {
                    finished = true;
                }
            } else {
                Vector3 moveDist = (moveTo - transform.position).normalized * speed * Time.deltaTime;
                if (moveDist.sqrMagnitude < (moveTo - transform.position).sqrMagnitude) {
                    transform.position += (moveTo - transform.position).normalized * speed * Time.deltaTime;
                } else {
                    finished = true;
                }
            }
        }
    }
}
