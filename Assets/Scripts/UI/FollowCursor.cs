using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 0;
        transform.position = pos;
    }
}
