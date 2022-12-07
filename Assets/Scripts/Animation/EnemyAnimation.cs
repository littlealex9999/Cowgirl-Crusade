using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class EnemyAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] float inRange = 100f;
    [SerializeField] float maxRange = 150f;

    [SerializeField] bool onTrigger = true;

    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onTrigger)
        {
            SearchForTarget();
        }
        
    }


    private void SearchForTarget()
    {
        float distance = GameManager.instance.DistanceFromPlayer(gameObject.transform);

        if (distance < inRange)
        {
            if (!enemy.hostile)
            {
                EnterCombat();
            }
        }else if (distance > maxRange)
        {
            if (enemy.hostile)
            {
                ExitCombat();
            }
        }
    }


    public void EnterCombat()
    {
        animator.SetTrigger("Hostile");
    }


    public void ExitCombat()
    {
        animator.SetTrigger("NotHostile");
    }


    void OnDrawGizmosSelected()
    {
        if (!onTrigger)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, inRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRange);
        }
        
    }

}
