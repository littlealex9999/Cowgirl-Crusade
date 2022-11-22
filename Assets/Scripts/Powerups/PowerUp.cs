using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private enum PowerupType { Activate, Door, Pickup, Ammo };

    [Header("Powerup Settings")]
    [SerializeField] private PowerupType powerup;
    [SerializeField] private GameObject effect;
    [SerializeField] private int amount = 1;



    
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
        if(other.CompareTag("Player"))
        {
            // Check power up type
                // Play audio clip    
                // Create instance of particle effect

                // Call function of relevant powerup
            
            Debug.Log("Player has hit powerup");
            Destroy(gameObject);

        }
    }

   // private void 


}
