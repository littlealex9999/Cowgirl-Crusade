using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjects : MonoBehaviour
{
    public List<GameObject> objectsToEnable;
    public bool disableActually;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && objectsToEnable != null) {
            foreach (GameObject go in objectsToEnable) {
                go.SetActive(!disableActually);
            }
        }
    }
}
