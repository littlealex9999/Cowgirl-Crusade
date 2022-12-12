using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PowerUp))] 

public class ShootablePowerup : Character
{
    PowerUp powerup;
    
    // Start is called before the first frame update
    void Start()
    {
        powerup = GetComponent<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnDeath()
    {
        base.OnDeath();
        Debug.Log("Balloon destroyed");
        powerup.ActivatePowerup(GameManager.instance.GetPlayer.GetComponent<Character>());
    }
}
