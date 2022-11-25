using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Player Specific"), Space] public float moveSpeed = 10;
    public Vector2 boundaryMoveMultipliers = new Vector2(0.8f, 0.5f);

    public float shootDistance = 10;
    public float bulletHomingSpeed = 100;

    Vector2 boundaries;
    Camera mainCamera;

    void Start()
    {
        base.Start();

        mainCamera = Camera.main;
        CalculateBoundaries();
    }

    protected override void Update()
    {
        base.Update();

        Move();
        Shoot();
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput.sqrMagnitude > 1) {
            moveInput.Normalize();
        }
        moveInput *= moveSpeed * Time.deltaTime;

        if (transform.localPosition.x + moveInput.x - mainCamera.transform.localPosition.x > boundaries.x) {
            moveInput.x = boundaries.x + mainCamera.transform.localPosition.x - transform.localPosition.x;
        } else if (transform.localPosition.x + moveInput.x - mainCamera.transform.localPosition.x < -boundaries.x) {
            moveInput.x = -boundaries.x + mainCamera.transform.localPosition.x - transform.localPosition.x;
        }

        if (transform.localPosition.y + moveInput.y - mainCamera.transform.localPosition.y > boundaries.y) {
            moveInput.y = boundaries.y + mainCamera.transform.localPosition.y - transform.localPosition.y;
        } else if (transform.localPosition.y + moveInput.y - mainCamera.transform.localPosition.y < -boundaries.y) {
            moveInput.y = -boundaries.y + mainCamera.transform.localPosition.y - transform.localPosition.y;
        }

        gameObject.transform.localPosition += moveInput;
    }

    void CalculateBoundaries()
    {
        Vector3 v3ViewPort = new Vector3(1, 1, -mainCamera.transform.localPosition.z);
        boundaries = transform.InverseTransformPoint(mainCamera.ViewportToWorldPoint(v3ViewPort));
        boundaries.x *= boundaryMoveMultipliers.x;
        boundaries.y *= boundaryMoveMultipliers.y;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //LayerMask.NameToLayer("RayHitLayer")
            if (Physics.Raycast(ray, out hitInfo, -mainCamera.transform.localPosition.z + shootDistance, 1 << 6)) {
                Bullet firedScript = base.Shoot(hitInfo.point);

                if (firedScript != null) { // bullet actually fired
                    firedScript.SetTarget(hitInfo.collider.gameObject);
                    firedScript.SetHomingSpeed(bulletHomingSpeed);
                }
            } else {
                Vector3 point = Input.mousePosition;
                point.z = -mainCamera.transform.localPosition.z + shootDistance;
                base.Shoot(mainCamera.ScreenToWorldPoint(point));
            }

            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * (-mainCamera.transform.localPosition.z + shootDistance), Color.cyan);
        }
    }
}
