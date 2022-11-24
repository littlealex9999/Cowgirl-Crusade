using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class PowerUp : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private PowerupStats powerupType;
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
            Character player = other.gameObject.GetComponent<Character>();
            player.AddPowerup(powerupType);

            Debug.Log("Player has received " + powerupType.name + "powerup");

                // Play audio clip    
                // Create instance of particle effect

            
            Destroy(gameObject);

        }
    }

   // private void 


}
