using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float moveSpeed = 1;

    public Vector2 boundaries;

    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateBoundaries();
    }

    protected override void Update()
    {
        base.Update();

        Move();
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
        if (transform.localPosition.x + moveInput.x > boundaries.x) {
            moveInput.x = boundaries.x - transform.localPosition.x;
        } else if (transform.localPosition.x + moveInput.x < -boundaries.x) {
            moveInput.x = -boundaries.x - transform.localPosition.x;
        }

        if (transform.localPosition.y + moveInput.y > boundaries.y) {
            moveInput.y = boundaries.y - transform.localPosition.y;
        } else if (transform.localPosition.y + moveInput.y < -boundaries.y) {
            moveInput.y = -boundaries.y - transform.localPosition.y;
        }

        gameObject.transform.localPosition += moveInput;
    }

    void CalculateBoundaries()
    {
        Vector3 v3ViewPort = new Vector3(1, 0, mainCamera.transform.localPosition.z);
        boundaries = (mainCamera.ViewportToWorldPoint(v3ViewPort) - mainCamera.transform.position) * 2;
    }
}
