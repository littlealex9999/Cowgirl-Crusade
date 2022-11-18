using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class RailsMovement : MonoBehaviour
{
    public BezierPath pathToFollow;
    [HideInInspector] public int movingToIndex = 0;

    public float speed = 1;
    public float rotationSpeed = 5f;

    public bool loopAtEnd = true;

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (movingToIndex < pathToFollow.getPath.Length) {
            RotateTowards(pathToFollow.getPath[movingToIndex]);

            float remainingMovement = speed * Time.deltaTime;

            while (remainingMovement > 0 && movingToIndex < pathToFollow.getPath.Length) {
                float targetDistance = (transform.position - pathToFollow.getPath[movingToIndex]).magnitude;

                if (remainingMovement - targetDistance >= 0) {
                    transform.position = pathToFollow.getPath[movingToIndex];
                    remainingMovement -= targetDistance;
                    ++movingToIndex;
                } else {
                    transform.position += (pathToFollow.getPath[movingToIndex] - transform.position).normalized * remainingMovement;
                    break;
                }
            }
        } else if (loopAtEnd) {
            movingToIndex = 0;
        }
    }

    void RotateTowards(Vector3 point)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(point - transform.position), rotationSpeed * Time.deltaTime);
    }
}
