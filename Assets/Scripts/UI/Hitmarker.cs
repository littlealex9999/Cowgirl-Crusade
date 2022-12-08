using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hitmarker : MonoBehaviour
{
    Image marker;

    float duration = 0.2f;


    
    // Start is called before the first frame update
    void Start()
    {
        marker = GetComponent<Image>();

    }


    public void HitEnemy()
    {
        
        marker.enabled = true;
        StartCoroutine(RemoveHitmarker());
        
    }

    IEnumerator RemoveHitmarker()
    {
        yield return new WaitForSeconds(duration);
        marker.enabled = false;

    }

}
