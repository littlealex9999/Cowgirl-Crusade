using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Character
{
    CM_FollowLeader myMoveScript;

    Character shootTarget;

    [SerializeField, Range(-1, 1), Space] float targetRelativeLookShootLimit = -0.1f;
    [SerializeField] float minDistanceBetweenTarget = 5;
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float shootDuration = 5;
    [SerializeField] float laserMoveSpeed = 5;
    [SerializeField] float forwardOffset = 1;
    [SerializeField, Space] float shootStartDistance = 5;
    float rotationSpeed = 4;
    bool firing;
    Vector3 shootingPos;
    Vector3 shootingDir;

    float shootTimer;
    LineRenderer lr;

    GameObject laserObj;

    protected override void Start()
    {
        base.Start();

        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();

        lr = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (hostile) {
            Shoot();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(shootTarget.transform.position - transform.position), resetRotationStrength * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.parent.forward), resetRotationStrength * Time.deltaTime);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (myMoveScript != null && myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }

        if (laserObj != null) {
            Destroy(laserObj);
        }
    }

    private void Shoot()
    {
        if (shootTarget != null &&
            (shootTarget.transform.position - transform.position).sqrMagnitude >= minDistanceBetweenTarget * minDistanceBetweenTarget && 
            Vector3.Dot(shootTarget.transform.forward, transform.forward) <= targetRelativeLookShootLimit) {
            if (firing) {
                shootingPos += shootingDir * laserMoveSpeed * Time.deltaTime;

                LaserLogic();

                if (shootTimer <= 0) {
                    firing = false;
                    Destroy(laserObj);
                }

                shootTimer -= Time.deltaTime;
            } else if (cldtimer <= 0) {
                cldtimer = shootCooldown;
                shootingPos = shootTarget.transform.localPosition;
                shootTimer = shootDuration;
                firing = true;

                shootingDir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;

                laserObj = Instantiate(enemyLaser);
                lr = laserObj.GetComponent<LineRenderer>();

                laserObj.transform.rotation = Quaternion.LookRotation((shootingPos + shootTarget.transform.parent.position) - (transform.position + transform.forward * forwardOffset)) * Quaternion.Euler(90, 0, 0);
            }
        } else if (laserObj != null) {
            Destroy(laserObj);
            firing = false;
        }
    }

    void LaserLogic()
    {
        if (laserObj != null) {
            laserObj.transform.localScale = new Vector3(laserObj.transform.lossyScale.x,
                                                        ((transform.position + transform.forward * forwardOffset) - shootTarget.transform.position).magnitude,
                                                        laserObj.transform.lossyScale.z) * 0.5f;

            if (shootTarget != null) {
                laserObj.transform.rotation = Quaternion.Slerp(laserObj.transform.rotation * Quaternion.Euler(-90, 0, 0),
                                                               Quaternion.LookRotation((shootingPos + shootTarget.transform.parent.position) - (transform.position + transform.forward * forwardOffset)),
                                                               rotationSpeed * Time.deltaTime) * Quaternion.Euler(90, 0, 0);
                laserObj.transform.position = (transform.position + transform.forward * forwardOffset) + laserObj.transform.lossyScale.y * laserObj.transform.up;

                if (lr != null) {
                    lr.SetPosition(0, transform.position + transform.forward * forwardOffset);
                    lr.SetPosition(1, laserObj.transform.position + laserObj.transform.up * laserObj.transform.lossyScale.y);
                }
            } else {
                Destroy(gameObject);
            }
        }
    }

    public override void SetTarget(Character target)
    {
        shootTarget = target;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistanceBetweenTarget);
    }
}
