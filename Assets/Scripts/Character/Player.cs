using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float moveSpeed = 1;

    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        Move();
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        gameObject.transform.localPosition += moveInput * moveSpeed * Time.deltaTime;
    }
}
