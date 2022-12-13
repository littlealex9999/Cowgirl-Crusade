using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitmarker : MonoBehaviour
{
    Image marker;

    [SerializeField] float duration = 0.2f;

    float lastHit = 0f;


    void Start()
    {
        marker = GetComponent<Image>();

    }


    void Update()
    {
        if (marker.enabled)
        {
            lastHit += Time.deltaTime;
            if(lastHit >= duration)
            {
                RemoveHitmarker();
            }
        }
    }

    public void HitEnemy()
    {
        lastHit = 0f;
        marker.enabled = true;
        
    }

    void RemoveHitmarker()
    {
        marker.enabled = false;
    }

}
