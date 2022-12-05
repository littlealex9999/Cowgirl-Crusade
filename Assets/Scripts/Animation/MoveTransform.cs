using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    [SerializeField] Vector3 moveSpeed = new Vector3(0f, 0f, 0f);
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed.x * Time.deltaTime, moveSpeed.y * Time.deltaTime, moveSpeed.z * Time.deltaTime, Space.Self);
    }
}
