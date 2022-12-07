using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExitCombat : MonoBehaviour
{
    public Enemy[] enemiesToSet;
    Enemy enemy;

    [SerializeField] bool triggeredByEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null && !triggeredByEnemy)
        {
            foreach (Enemy e in enemiesToSet)
            {
                e.SetTarget(null);
                e.ExitCombat();

            }
        }else if(other.tag == "Enemy" && triggeredByEnemy)
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.SetTarget(null);
            enemy.ExitCombat();
        }
    }

}
