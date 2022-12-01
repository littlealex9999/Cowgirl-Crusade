using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggle : MonoBehaviour
{
    public Vector3 maxJiggle;
    public float speed = 1;

    Vector3 moved = new Vector3();

    void Update()
    {
        Vector3 finalMove = new Vector3();
        if (maxJiggle.x != 0) {
            finalMove.x = speed * Mathf.Sign(maxJiggle.x);
        }

        if (maxJiggle.y != 0) {
            finalMove.y = speed * Mathf.Sign(maxJiggle.y);
        }

        if (maxJiggle.z != 0) {
            finalMove.z = speed * Mathf.Sign(maxJiggle.z);
        }
        finalMove *= Time.deltaTime;

        moved += finalMove;

        if (moved.x > Mathf.Abs(maxJiggle.x) || moved.x < -Mathf.Abs(maxJiggle.x)) {
            finalMove.x = (maxJiggle.x - moved.x);
            moved.x = maxJiggle.x;
            maxJiggle.x *= -1;
        }

        if (moved.y > Mathf.Abs(maxJiggle.y) || moved.y < -Mathf.Abs(maxJiggle.y)) {
            finalMove.y = (maxJiggle.y - moved.y);
            moved.y = maxJiggle.y;
            maxJiggle.y *= -1;
        }

        if (moved.z > Mathf.Abs(maxJiggle.z) || moved.z < -Mathf.Abs(maxJiggle.z)) {
            finalMove.z = (maxJiggle.z - moved.z);
            moved.z = maxJiggle.z;
            maxJiggle.z *= -1;
        }

        transform.localPosition += finalMove;
    }
}
