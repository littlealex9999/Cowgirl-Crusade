using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] float enemyCollisionDamage = 70f;


    
    
    // Start is called before the first frame update
    void Start()
    {
        

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if(other.gameObject.GetComponent<Terrain>() != null)
        {
            Debug.Log("Collision with terrain");

        }else if(other.gameObject.GetComponent<Hazard>() ){

        }



        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            DamagePlayer(enemyCollisionDamage);
            // Play explosion particle effect
            // Play explosion sound effect
            
        }

    }


    private void DamagePlayer(float amount)
    {
        // GetComponent<Player>().TakeDamage(amount);

    }


}
