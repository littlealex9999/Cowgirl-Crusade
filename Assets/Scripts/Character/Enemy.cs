using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] RailsEnemyMovement myMoveScript;
    Character shootTarget;

    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();

        Shoot();
    }

    void Shoot()
    {
        if (shootTarget != null) {
            base.Shoot(shootTarget.transform.position);
        }
    }

    public void FindTarget()
    {
        if (myMoveScript.thingToFollow != null) {
            shootTarget = myMoveScript.thingToFollow.GetComponentInChildren<Character>();
        } else {
            shootTarget = null;
        }
    }
}
