using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyTarget_PLAYER : MonoBehaviour
{
    public Enemy[] enemiesToSet;
    public Character newTarget;

    public bool justFigureOutWhoThePlayerIs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null) {
            foreach (Enemy e in enemiesToSet) {
                if (justFigureOutWhoThePlayerIs) {
                    e.SetTarget(other.GetComponentInChildren<Player>());
                } else {
                    e.SetTarget(newTarget);
                }
            }
        }
    }
}
