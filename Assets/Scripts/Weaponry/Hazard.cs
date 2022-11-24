using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] GameObject destructionPrefab;

    [SerializeField] float dmg = 10;


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
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null)
        {
            character.TakeDamage(dmg);
            if (character.gameObject.CompareTag("Player"))
            {

                Destruct();
            }
        }
    }


    public void Destruct()
    {
        // Object.Instantiate(destructionPrefab);

        Destroy(gameObject);
    }

}
