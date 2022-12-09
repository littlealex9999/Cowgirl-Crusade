using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Character
{
    CM_FollowLeader myMoveScript;
    Character shootTarget;

    [SerializeField, Range(0, 1)] float spreadFrequency = 0.5f; // 0 = never spread, 1 = always spread
    [SerializeField] float spread = 3;
    [SerializeField, Space] bool lookAtTarget = true;

    protected override void Start()
    {
        base.Start();

        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();
        dontRotate = lookAtTarget;
    }

    void Update()
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
        if (myMoveScript!= null && myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }
    }

    private void Shoot()
    {
        if (shootTarget != null) {
            Vector3 spreadVector = new Vector3();

            if (Random.Range(0, 1) <= spreadFrequency) {
                spreadVector += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            }

            base.Shoot(shootTarget.transform.position + spreadVector, shootTarget.transform.parent);
        }
    }

    public void FindTarget()
    {
        if (myMoveScript.leader != null) {
            shootTarget = myMoveScript.leader.GetComponentInChildren<Character>();
        } else {
            shootTarget = null;
        }
    }

    public override void SetTarget(Character chara)
    {
        shootTarget = chara;
    }
}
