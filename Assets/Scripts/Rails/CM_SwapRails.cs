using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_SwapRails : MonoBehaviour
{
    [SerializeField] CinemachineSmoothPath newPath;
    [SerializeField] int pointToStartAt = 0;

    [SerializeField, Space] bool onlyPlayer;
    [SerializeField] bool anythingButPlayer;
    [SerializeField] bool deleteOnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer && other.tag != "Player" || anythingButPlayer && other.tag == "Player")
            return;

        CinemachineDollyCart dc = other.GetComponent<CinemachineDollyCart>();
        if (dc != null) {
            dc.m_Path = newPath;
            dc.m_Position = pointToStartAt;

            if (deleteOnTrigger) {
                Destroy(gameObject);
            }
        }
    }
}
