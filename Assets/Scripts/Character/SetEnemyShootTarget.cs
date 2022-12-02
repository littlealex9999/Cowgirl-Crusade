using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyShootTarget : MonoBehaviour
{
    [SerializeField] Character target;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null) {
            enemy.SetTarget(target);
        }
    }
}
