using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemy : Character
{
    CM_FollowLeader myMoveScript;

    [SerializeField] float explosionRadius;
    [SerializeField] float damage;

    #region Unity functions
    //void Start()
    //{
    //    OnStart();
    //}

    void OnDestroy()
    {
        if (myMoveScript.leader.tag == "Player") {
            Player.RemoveAttacker();
        }
    }
    private void FixedUpdate()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, explosionRadius);
        if (collisions != null) {
            foreach (Collider other in collisions) {
                if (other.tag == "Player") {
                    other.GetComponent<Player>().TakeDamage(damage);
                    TakeDamage(getCurrentHealth + getCurrentShield, 0, false);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    #endregion

    #region Custom Unity Overrides
    protected override void OnStart()
    {
        base.OnStart();
        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();
    }


    #endregion
}
