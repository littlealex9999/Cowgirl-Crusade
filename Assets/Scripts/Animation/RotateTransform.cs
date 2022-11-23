using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    [SerializeField] Vector3 spinSpeed = new Vector3(0f, 0f, 0f);
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinSpeed.x * Time.deltaTime, spinSpeed.y * Time.deltaTime, spinSpeed.z * Time.deltaTime, Space.Self);
    }
}
