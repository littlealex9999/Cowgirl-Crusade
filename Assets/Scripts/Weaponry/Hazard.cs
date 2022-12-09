using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject destructionPrefab;

    [SerializeField, Min(0)] float dmg = 10;
    [SerializeField, Min(0)] float invincibilityDuration = 0;

    [SerializeField] bool playerOnly = false;
    [SerializeField] bool destroyOnHit = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerOnly && other.tag != "Player") {
            return;
        }

        Character character = other.gameObject.GetComponent<Character>();

        if (character != null) {
            character.TakeDamage(dmg, invincibilityDuration);
            if (destroyOnHit) {
                Destruct();
            }
        }
    }

    public void Destruct()
    {
        if (destructionPrefab != null) {
            Instantiate(destructionPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}
