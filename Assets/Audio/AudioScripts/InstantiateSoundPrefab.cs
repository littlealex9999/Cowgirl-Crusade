using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSoundPrefab : MonoBehaviour
{
    [SerializeField] GameObject soundPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Object.Instantiate(soundPrefab);
    }

}
