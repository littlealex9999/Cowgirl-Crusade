using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemy : Character
{
    CM_FollowLeader myMoveScript;

    [SerializeField] float explosionRadius;
    [SerializeField] float damage;

    protected override void Start()
    {
        base.Start();

        myMoveScript = transform.parent.GetComponent<CM_FollowLeader>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
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
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
