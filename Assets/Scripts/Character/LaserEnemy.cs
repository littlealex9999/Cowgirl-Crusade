using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Character
{
    [SerializeField] CM_FollowLeader myMoveScript;

    Character target;

    [SerializeField, Space] GameObject enemyLaser;
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

    protected override void Start()
    {
        base.Start();

        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();

        lr = GetComponent<LineRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        Shoot();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }
    }

    private void Shoot()
    {
        if (target != null) {
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
                shootingPos = target.transform.localPosition;
                shootTimer = shootDuration;
                firing = true;

                laserObj = Instantiate(enemyLaser);
                lr = laserObj.GetComponent<LineRenderer>();

                laserObj.transform.rotation = Quaternion.LookRotation((shootingPos + target.transform.parent.position) - (transform.position + transform.forward * forwardOffset));
            }
        }
    }

    void LaserLogic()
    {
        if (laserObj != null) {
            laserObj.transform.localScale = new Vector3(laserObj.transform.lossyScale.x,
                                                        ((transform.position + transform.forward * forwardOffset) - target.transform.position).magnitude,
                                                        laserObj.transform.lossyScale.z) * 0.5f;

            if (target != null) {
                laserObj.transform.rotation = Quaternion.Slerp(laserObj.transform.rotation * Quaternion.Euler(-90, 0, 0),
                                                               Quaternion.LookRotation((shootingPos + target.transform.parent.position) - (transform.position + transform.forward * forwardOffset)),
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
        this.target = target;
    }
}
