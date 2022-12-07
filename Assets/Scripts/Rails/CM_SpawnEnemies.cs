using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CM_SpawnEnemies : MonoBehaviour
{
    [SerializeField] CM_FollowLeader[] enemyCarts;

    public bool tpIntoPlace = true;
    public bool despawnThemActually;
    public bool deleteOnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            foreach (CM_FollowLeader fl in enemyCarts) {
                fl.gameObject.SetActive(!despawnThemActually);

                if (!despawnThemActually && tpIntoPlace && fl.enabled) {
                    fl.thisCart.m_Position = fl.leader.m_Position + fl.targetPointsDifference;
                }
            }

            if (deleteOnPlayerEnter)
                Destroy(gameObject);
        }
    }
}
