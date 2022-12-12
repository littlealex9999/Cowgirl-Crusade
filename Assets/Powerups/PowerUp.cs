using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]


public class PowerUp : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private PowerupStats powerupType;
    [SerializeField] private GameObject effect;
    [SerializeField] bool onTrigger = true;

    private AudioSource audioSource;


    

    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (onTrigger)
        {
            if (other.CompareTag("Player"))
            {
                Character player = other.gameObject.GetComponentInChildren<Character>();

               // ActivatePowerup(player);
            }

        }
        
    }


    public void ActivatePowerup(Character player)
    {
        player.AddPowerup(powerupType);

        Debug.Log("Player has received " + powerupType.name + " powerup.");

        if (effect != null)
        {
            Object.Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        }
        

        Destroy(gameObject);
    }

}
