using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyTarget : MonoBehaviour
{
    public Enemy[] enemiesToSet;
    public Character newTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null) {
            foreach (Enemy e in enemiesToSet) {
                e.SetTarget(newTarget);
            }
        }
    }
}
