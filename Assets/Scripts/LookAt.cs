using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;
    
    public bool aiming;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = GameManager.instance.GetPlayer;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming && target != null)
        {
            transform.LookAt(target.transform);
            
        }
    }
}
