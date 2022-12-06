using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform target;

    Vector3 startingRotation;

    public float range;

    public bool aiming = false;


    /*

     THIS SCRIPT IS NOT READY TO BE USED

     Please use the LookAt Script instead for now
    
     */



    // Start is called before the first frame update
    void Start()
    {
        startingRotation = transform.rotation.eulerAngles;

        InvokeRepeating("SearchForTarget", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming)
        {
            //transform.rotation = 
        }
    }



    private void SearchForTarget()
    {
        float distance = GameManager.instance.DistanceFromPlayer(gameObject.transform);

        if (distance < range)
        {
            aiming = true;
        }
        else
        {
            if (aiming)
            {
                aiming = false;
                ReturnToStartRotation();
            }
                
        }
    }


    private void ReturnToStartRotation()
    {
        //transform.rotation = Quaternion.RotateTowards
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
