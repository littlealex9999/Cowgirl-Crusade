using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] float minRange = 100f;
    [SerializeField] float maxRange = 150f;

    [SerializeField] bool onTrigger = true;

    [SerializeField, Space] bool alsoSetShootTarget;

    [SerializeField] float setCooldownTo;
    [SerializeField] float setCooldownRandomRange;
    [SerializeField] float cooldownMinimum;

    Character enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Character>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!onTrigger) {
            SearchForTarget();
        }
    }

    private void SearchForTarget()
    {
        float distance = (GameManager.instance.GetPlayer.transform.position - transform.position).sqrMagnitude;

        if (distance < minRange * minRange) {
            if (!enemy.hostile) {
                EnterCombat();
            }
        } else if (distance > maxRange * maxRange) {
            if (enemy.hostile) {
                ExitCombat();
            }
        }
    }


    public void EnterCombat()
    {
        animator.SetTrigger("Hostile");

        if (alsoSetShootTarget) {
            enemy.SetTarget(GameManager.instance.GetPlayer.GetComponentInChildren<Player>());
            enemy.SetCurrentCooldown(CalculateCooldown());
            enemy.hostile = true;
        }
    }


    public void ExitCombat()
    {
        animator.SetTrigger("NotHostile");

        if (alsoSetShootTarget) {
            enemy.SetTarget(null);
            enemy.hostile = false;
        }
    }


    void OnDrawGizmosSelected()
    {
        if (!onTrigger) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRange);
        }
    }


    float CalculateCooldown()
    {
        return Mathf.Clamp(setCooldownTo + Random.Range(-setCooldownRandomRange, setCooldownRandomRange), cooldownMinimum, setCooldownTo + setCooldownRandomRange);
    }
}
