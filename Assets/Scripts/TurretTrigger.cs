using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTrigger : MonoBehaviour
{
    [SerializeField] LookAtTarget[] turrets;

    private enum TurretActivation { Enable, Disable };

    [SerializeField] private TurretActivation turretActivation;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (turretActivation)
            {
                case TurretActivation.Enable:
                    EnableTurrets(other.gameObject);
                    break;
                case TurretActivation.Disable:
                    DisableTurrets();
                    break;
            }
        }
    }

    void EnableTurrets(GameObject player)
    {
        foreach (LookAtTarget t in turrets)
        {
            t.target = player;
        }
    }
    
    void DisableTurrets()
    {
        foreach (LookAtTarget t in turrets)
        {
            t.target = null;
        }
    }

}
