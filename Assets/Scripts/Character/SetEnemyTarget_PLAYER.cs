using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyTarget_PLAYER : MonoBehaviour
{
    public Character[] enemiesToSet;
    Character enemy;
    public Character newTarget;

    public float setCooldownTo;
    public float setCooldownRandomRange;
    public float cooldownMinimum;

    [Space] public bool justFigureOutWhoThePlayerIs;

    [SerializeField] bool triggeredByEnemy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null && !triggeredByEnemy) {
            foreach (Character e in enemiesToSet) {
                if (justFigureOutWhoThePlayerIs) {
                    Player player = other.GetComponentInChildren<Player>();
                    e.SetTarget(player);

                    if (player == null) {
                        e.ExitCombat();
                    } else {
                        e.EnterCombat();
                        e.SetCurrentCooldown(CalculateCooldown());
                    }
                } else {
                    e.SetTarget(newTarget);

                    if (newTarget == null) {
                        e.ExitCombat();
                    } else {
                        e.EnterCombat();
                        e.SetCurrentCooldown(CalculateCooldown());
                    }
                }
            }
        } else if (other.tag == "Enemy" && triggeredByEnemy) {
            enemy = other.gameObject.GetComponent<Character>();
            if (justFigureOutWhoThePlayerIs) {
                Player player = other.GetComponentInChildren<Player>();
                enemy.SetTarget(player);

                if (player == null) {
                    enemy.ExitCombat();
                } else {
                    enemy.EnterCombat();
                    enemy.SetCurrentCooldown(CalculateCooldown());
                }
            } else {
                enemy.SetTarget(newTarget);

                if (newTarget == null) {
                    enemy.ExitCombat();
                } else {
                    enemy.EnterCombat();
                    enemy.SetCurrentCooldown(CalculateCooldown());
                }
            }
        }
    }


    float CalculateCooldown()
    {
        return Mathf.Clamp(setCooldownTo + Random.Range(-setCooldownRandomRange, setCooldownRandomRange), cooldownMinimum, setCooldownTo + setCooldownRandomRange);
    }
}
