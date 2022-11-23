using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin_Powerup : MonoBehaviour
{
    [SerializeField] int spinSpeed = 100;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.Self);
    }
}
