using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;

public class RailsMovement : MonoBehaviour
{
    public BezierPath pathToFollow;
    [HideInInspector] public int movingToIndex = 0;

    public float speed = 1;
    public float rotationSpeed = 5f;
    public int rotationPointOffset = 1;

    public bool loopAtEnd = true;

    void Start()
    {
        
    }

    protected virtual void Update()
    {
        // if we have somewhere to move to, calculate our speed with delta time
        // then find the distance to the next point, and teleport to it if we have more speed than required
        // finally, if we don't have enough speed, move towards the next point and stop
        // this gives us consistant move speed through the bezier curve

        if (pathToFollow != null && movingToIndex < pathToFollow.getPath.Length) {
            if (movingToIndex + rotationPointOffset >= 0 && movingToIndex + rotationPointOffset < pathToFollow.getPath.Length) {
                RotateTowards(pathToFollow.getPath[movingToIndex + rotationPointOffset]);
            }

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
        } 
        
        if (pathToFollow != null && loopAtEnd && movingToIndex >= pathToFollow.getPath.Length) {
            movingToIndex = 0;
        }
    }

    void RotateTowards(Vector3 point)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(point - transform.position), rotationSpeed * Time.deltaTime);
    }

    public virtual void SetSpeed(float value)
    {
        speed = value;
    }

    public virtual void SetRotationSpeed(float value)
    {
        rotationSpeed = value;
    }
}
