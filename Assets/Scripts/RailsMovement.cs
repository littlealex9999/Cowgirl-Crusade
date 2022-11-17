using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class RailsMovement : MonoBehaviour
{
    [SerializeField] BezierPath pathToFollow;
    int movingToIndex = 0;

    public float speed = 1;

    void Start()
    {
        
    }

    void Update()
    {
        if (movingToIndex <= pathToFollow.getPath.Length - 1) {
            gameObject.transform.LookAt(pathToFollow.getPath[movingToIndex]);

            float remainingMovement = speed * Time.deltaTime;

            while (remainingMovement > 0) {
                float targetDistance = (transform.position - pathToFollow.getPath[movingToIndex]).magnitude;

                if (remainingMovement - targetDistance >= 0) {
                    transform.position = pathToFollow.getPath[movingToIndex];
                    remainingMovement -= targetDistance;
                    ++movingToIndex;
                } else {
                    transform.position += transform.forward * remainingMovement;
                    break;
                }
            }
        }
    }
}
