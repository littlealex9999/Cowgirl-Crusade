using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class MechAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] float inRange = 100f;
    [SerializeField] float maxRange = 150f;

    [SerializeField] bool onTrigger = true;

    bool hostile = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onTrigger)
        {
            SearchForTarget();
        }
        
    }


    private void SearchForTarget()
    {
        float distance = GameManager.instance.DistanceFromPlayer(gameObject.transform);

        if (distance < inRange)
        {
            if (!hostile)
            {
                OpenAnimation();
            }
        }else if (distance > maxRange)
        {
            if (hostile)
            {
                CloseAnimation();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.material != null)
        {
            if (other.material.name.Contains("Activate")) // The projectile has hit an enemy
            {
                OpenAnimation();
            }
            else if (other.material.name.Contains("Deactivate"))
            {
                CloseAnimation();
            }
            else if (other.material.name.Contains("Destroy"))
            {
                Destroy(gameObject);
            }

        }
    }



    void OpenAnimation()
    {
        hostile = true;
        animator.SetTrigger("Open");
    }


    void CloseAnimation()
    {
        hostile = false;
        animator.SetTrigger("Close");
    }


    void OnDrawGizmosSelected()
    {
        if (!onTrigger)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, inRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxRange);
        }
        
    }

}
