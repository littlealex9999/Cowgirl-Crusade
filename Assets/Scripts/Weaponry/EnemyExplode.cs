using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    public float explosionSize = 5;
    public float damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        // explosion size can be different from trigger size
        if (other.tag == "Player") {
            Collider[] objectsHit = Physics.OverlapSphere(transform.position, explosionSize);
            for (int i = 0; i < objectsHit.Length; ++i) {
                if (objectsHit[i].tag == "Player") {
                    Character hitChara = objectsHit[i].GetComponent<Character>();

                    if (hitChara != null) {
                        hitChara.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
