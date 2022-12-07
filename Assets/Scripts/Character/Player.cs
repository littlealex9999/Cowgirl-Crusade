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
    Vector3 initialOffset;

    static int numEnemiesAttacking;

    void Start()
    {
        base.Start();

        mainCamera = Camera.main;
        initialOffset = mainCamera.transform.localPosition;
        CalculateBoundaries();

        numEnemiesAttacking = 0;
    }

    protected override void Update()
    {
        base.Update();

        Move();
        Shoot();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameManager.instance.GetScore.ResetPoints();
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveInput.sqrMagnitude > 1) {
            moveInput.Normalize();
        }
        moveInput *= moveSpeed * Time.deltaTime;

        if (transform.localPosition.x + moveInput.x - initialOffset.x > boundaries.x) {
            moveInput.x = boundaries.x + initialOffset.x - transform.localPosition.x;
        } else if (transform.localPosition.x + moveInput.x - initialOffset.x < -boundaries.x) {
            moveInput.x = -boundaries.x + initialOffset.x - transform.localPosition.x;
        }

        if (transform.localPosition.y + moveInput.y - initialOffset.y > boundaries.y) {
            moveInput.y = boundaries.y + initialOffset.y - transform.localPosition.y;
        } else if (transform.localPosition.y + moveInput.y - initialOffset.y < -boundaries.y) {
            moveInput.y = -boundaries.y + initialOffset.y - transform.localPosition.y;
        }

        gameObject.transform.localPosition += moveInput;
    }

    void CalculateBoundaries()
    {
        Vector3 v3ViewPort = new Vector3(1, 1, -initialOffset.z);
        boundaries = transform.InverseTransformPoint(mainCamera.ViewportToWorldPoint(v3ViewPort));
        boundaries.x *= boundaryMoveMultipliers.x;
        boundaries.y *= boundaryMoveMultipliers.y;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0)) {
            Vector3 shootTo = GetCursorPoint(out bool hitEnemy, out RaycastHit hitInfo);
            if (hitEnemy) {
                Bullet firedScript = base.Shoot(shootTo);

                if (firedScript != null) { // bullet actually fired
                    firedScript.SetTarget(hitInfo.collider.gameObject);
                    firedScript.SetHomingSpeed(bulletHomingSpeed);
                }
            } else {
                base.Shoot(shootTo);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            base.SpawnSpecialProjectile();
        }
    }

    public Vector3 GetCursorPoint(out bool hitEnemy, out RaycastHit hitInfo)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, -initialOffset.z + shootDistance, 1 << 6)) {
            hitEnemy = true;
            return hitInfo.point;
        } else {
            hitEnemy = false;

            Vector3 point = Input.mousePosition;
            point.z = -initialOffset.z + shootDistance;
            return mainCamera.ScreenToWorldPoint(point);
        }
    }


    public override bool TakeDamage(float damage, float setInvincibleTime = 0)
    {
        
        //GameManager.instance.ScreenShake(shakeIntensity, shakeDuration);
        // Red border on screen
        return base.TakeDamage(damage, setInvincibleTime);
    }



    public static void AddAttacker()
    {
        ++numEnemiesAttacking;
    }

    public static void RemoveAttacker()
    {
        if (numEnemiesAttacking > 0)
            --numEnemiesAttacking;
    }

    public static int GetNumAttackers()
    {
        return numEnemiesAttacking;
    }
}
