using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Character
{
    CM_FollowLeader myMoveScript;

    Character shootTarget;

    [SerializeField, Range(-1, 1), Space] float targetRelativeLookShootLimit = -0.1f;
    [SerializeField] float minDistanceBetweenTarget = 5;
    [SerializeField, Space] GameObject enemyLaser;
    [SerializeField] float shootStartDistance = 5;
    [SerializeField] float shootDuration = 5;
    [SerializeField] float laserMoveSpeed = 5;
    [SerializeField] float forwardOffset = 1;
    float rotationSpeed = 4;
    bool firing;
    Vector3 shootingPos;
    Vector3 shootingDir;

    float shootTimer;
    LineRenderer lr;

    GameObject laserObj;

    #region Unity Functions
    //void Start()
    //{
    //    OnStart();
    //}

    //void Update()
    //{
    //    OnUpdate();
    //}

    void OnDestroy()
    {
        if (myMoveScript != null && myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }

        if (laserObj != null) {
            Destroy(laserObj);
        }
    }
    #endregion

    #region Custom Unity Overrides
    protected override void OnStart()
    {
        base.OnStart();

        if (transform.parent != null) {
            myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();
        }

        lr = GetComponent<LineRenderer>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (hostile) {
            Shoot();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(shootTarget.transform.position - transform.position), resetRotationStrength * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.parent.forward), resetRotationStrength * Time.deltaTime);
        }
    }
    #endregion

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
                shootingPos = shootTarget.transform.localPosition + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)).normalized * shootStartDistance;
                shootTimer = shootDuration;
                firing = true;

                shootingDir = (shootTarget.transform.localPosition - shootingPos).normalized;

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
                                                        ((transform.position + transform.forward * forwardOffset) - shootTarget.transform.position).magnitude * 2,
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
