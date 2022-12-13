using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableProp : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void ShotByBullet(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Shot by bullet");
        
        Object.Instantiate(hitEffect, transform.position, transform.rotation);
    }

}
