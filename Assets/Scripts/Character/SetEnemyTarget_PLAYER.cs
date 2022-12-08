using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyTarget_PLAYER : MonoBehaviour
{
    public Character[] enemiesToSet;
    Character enemy;
    public Character newTarget;

    public bool justFigureOutWhoThePlayerIs;

    [SerializeField] bool triggeredByEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null && !triggeredByEnemy) {
            foreach (Character e in enemiesToSet) {
                if (justFigureOutWhoThePlayerIs) {
                    e.SetTarget(other.GetComponentInChildren<Player>());
                    e.EnterCombat();

                } else {
                    e.SetTarget(newTarget);
                    e.EnterCombat();

                }

            }

        } else if (other.tag == "Enemy" && triggeredByEnemy)
        {
            enemy = other.gameObject.GetComponent<Character>();
            if (justFigureOutWhoThePlayerIs)
            {
                enemy.SetTarget(GameManager.instance.GetPlayer.GetComponent<Character>());
                enemy.EnterCombat();
            }
            else
            {
                enemy.SetTarget(newTarget);
                enemy.EnterCombat();
            }

        }
    }

}
