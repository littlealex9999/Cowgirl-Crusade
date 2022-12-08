using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExitCombat : MonoBehaviour
{
    public BasicEnemy[] enemiesToSet;
    BasicEnemy enemy;

    [SerializeField] bool triggeredByEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null && !triggeredByEnemy)
        {
            foreach (BasicEnemy e in enemiesToSet)
            {
                e.SetTarget(null);
                e.ExitCombat();

            }
        }else if(other.tag == "Enemy" && triggeredByEnemy)
        {
            enemy = other.gameObject.GetComponent<BasicEnemy>();
            enemy.SetTarget(null);
            enemy.ExitCombat();
        }
    }

}
