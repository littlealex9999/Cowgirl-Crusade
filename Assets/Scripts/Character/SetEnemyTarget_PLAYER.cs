using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyTarget_PLAYER : MonoBehaviour
{
    public Character[] enemiesToSet;
    public Character newTarget;

    public bool justFigureOutWhoThePlayerIs;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemiesToSet != null) {
            foreach (Character e in enemiesToSet) {
                if (justFigureOutWhoThePlayerIs) {
                    e.SetTarget(other.GetComponentInChildren<Player>());
                } else {
                    e.SetTarget(newTarget);
                }
            }
        }
    }
}
