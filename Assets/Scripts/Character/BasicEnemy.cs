using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Character
{
    CM_FollowLeader myMoveScript;
    Character shootTarget;

    [SerializeField, Range(-1, 1), Space] float targetRelativeLookShootLimit = -0.1f;
    [SerializeField] float minDistanceBetweenTarget = 5;
    [SerializeField, Range(0, 1)] float spreadFrequency = 0.5f; // 0 = never spread, 1 = always spread
    [SerializeField] float spread = 3;
    [SerializeField, Space] bool lookAtTarget = true;

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
        if (myMoveScript!= null && myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }
    }
    #endregion

    #region Custom Unity Overrides
    protected override void OnStart()
    {
        base.OnStart();
        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();
        dontRotate = lookAtTarget;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, minDistanceBetweenTarget);
    }
}
