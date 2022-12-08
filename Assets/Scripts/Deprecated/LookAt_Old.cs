using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt_Old : MonoBehaviour
{
    public GameObject target;

    
    [SerializeField] bool AimOnlyWhenInRange;

    [SerializeField] int inRange = 150;
    [SerializeField] int maxRange = 200;

    bool hostile = false; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = GameManager.instance.GetPlayer;
            // Debug.Log(target.name);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (AimOnlyWhenInRange)
        {
            CheckDistance();

        }
        else
        {
            AimAtTarget();
        }
    }


    void CheckDistance()
    {
        float distance = GameManager.instance.DistanceFromPlayer(gameObject.transform);

        if (distance < inRange)
        {
            hostile = true;
        }
        else if (distance > maxRange)
        {
            hostile = false;
        }

        if (hostile)
        {
            AimAtTarget();
        }
    }

    void AimAtTarget()
    {
        transform.LookAt(target.transform);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, inRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }

}
