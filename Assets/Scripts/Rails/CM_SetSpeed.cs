using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_SetSpeed : MonoBehaviour
{
    public CinemachineDollyCart[] cartsToSet;
    public bool playerOnlyTrigger;
    public bool destroyOnTrigger;

    [Space] public float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (playerOnlyTrigger && other.tag != "Player") {
            return;
        }

        foreach (CinemachineDollyCart dc in cartsToSet) {
            dc.m_Speed = speed;
        }

        if (destroyOnTrigger) {
            Destroy(gameObject);
        }
    }
}
