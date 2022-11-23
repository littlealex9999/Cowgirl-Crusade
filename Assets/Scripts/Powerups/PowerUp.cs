using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class PowerUp : MonoBehaviour
{

    private enum PowerupType {  };

    [Header("Powerup Settings")]
    [SerializeField] private PowerupType powerup;
    [SerializeField] private GameObject effect;
    [SerializeField] private int amount = 1;

    private AudioSource audioSource;


    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Get player's stat script
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
