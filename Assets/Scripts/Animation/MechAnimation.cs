using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class MechAnimation : MonoBehaviour
{
    Animator animator;

    [SerializeField] float openRange = 100f;
    [SerializeField] float closeRange = 150f;

    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        InvokeRepeating("SearchForTarget", 0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SearchForTarget()
    {
        float distance = GameManager.instance.DistanceFromPlayer(gameObject.transform);

        if (distance < openRange)
        {
            if (!open)
            {
                OpenAnimation();
            }
        }else if (distance > closeRange)
        {
            if (open)
            {
                CloseAnimation();
            }
        }
    }


    void OpenAnimation()
    {
        open = true;
        animator.SetTrigger("Open");
    }


    void CloseAnimation()
    {
        open = false;
        animator.SetTrigger("Close");
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, openRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, closeRange);
    }

}
