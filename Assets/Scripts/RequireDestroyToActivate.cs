using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireDestroyToActivate : MonoBehaviour
{
    public GameObject[] toDestroy;

    [Space] public GameObject[] objectsToActivate;
    public MonoBehaviour[] scriptsToActivate;
    public Collider[] collidersToActivate;

    void Update()
    {
        foreach (GameObject go in toDestroy) {
            if (go != null) {
                return;
            }
        }

        if (objectsToActivate != null) {
            foreach (GameObject go in objectsToActivate) {
                go.SetActive(true);
            }
        }
        if (scriptsToActivate != null) {
            foreach (MonoBehaviour mono in scriptsToActivate) {
                mono.enabled = true;
            }
        }
        if (collidersToActivate != null) {
            foreach (Collider c in collidersToActivate) {
                c.enabled = true;
            }
        }

        Destroy(this);
    }
}
