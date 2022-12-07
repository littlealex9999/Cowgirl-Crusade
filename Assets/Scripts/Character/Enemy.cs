using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] RailsEnemyMovement myMoveScript;
    Character shootTarget;

    EnemyAnimation hostileAnimation;
    public bool hostile = false;

    [SerializeField, Range(0, 1)] float spreadFrequency = 0.5f; // 0 = never spread, 1 = always spread
    [SerializeField] float spread = 3;

    void Start()
    {
        base.Start();

        if (GetComponent<EnemyAnimation>() != null)
        {
            hostileAnimation = GetComponent<EnemyAnimation>();
        }
    }

    void Update()
    {
        base.Update();

        if (hostile)
        {
            Shoot();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (myMoveScript.thingToFollow.tag == "Player") {
            Player.RemoveAttacker();
        }
    }

    void Shoot()
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
        if (myMoveScript.thingToFollow != null) {
            shootTarget = myMoveScript.thingToFollow.GetComponentInChildren<Character>();
        } else {
            shootTarget = null;
        }
    }

    public void SetTarget(Character chara)
    {
        shootTarget = chara;
    }

    public void EnterCombat()
    {
        hostile = true;

        if(hostileAnimation != null)
        {
            hostileAnimation.EnterCombat();
        }
    }

    public void ExitCombat()
    {
        hostile = false;

        if (hostileAnimation != null)
        {
            hostileAnimation.ExitCombat();
        }
        
    }

}
