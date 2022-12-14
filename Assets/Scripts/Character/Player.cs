using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region variables
    [Header("Player Specific"), Space] public float moveSpeed = 10;
    public Vector2 boundaryMoveMultipliers = new Vector2(0.8f, 0.5f);

    [SerializeField] VirtualCamera virtualCam;

    public float shootDistance = 10;
    public float bulletHomingSpeed = 100;

    Vector2 boundaries;
    Camera mainCamera;

    Vector3 initialOffset;

    static int numEnemiesAttacking;

    bool controlsEnabled = true;
    bool hitEnemy;
    #endregion

    #region properties
    public bool ControlsEnabled { get { return controlsEnabled; } set { controlsEnabled = value; } }
    public VirtualCamera GetVirtualCamera { get { return virtualCam; } }
    public bool CursorOverEnemy { get { return hitEnemy; } }
    #endregion

    #region Unity Functions
    //void Start()
    //{
    //    OnStart();
    //}

    //void Update()
    //{
    //    OnUpdate();
    //}
    #endregion

    #region Custom Unity Overrides
    protected override void OnStart()
    {
        base.OnStart();
        mainCamera = Camera.main;
        initialOffset = mainCamera.transform.localPosition;
        CalculateBoundaries();

        numEnemiesAttacking = 0;

        if (healthMeter != null) {
            healthMeter.runOnEmptyMeter += GameManager.instance.GameOver;
            healthMeter.runOnAnimationEnd += ResetHealthBarColor;
        }

        weaponHeat = GetComponent<WeaponHeat>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (controlsEnabled)
        {
            Move();
            Shoot();
        }
    }

    #endregion

    #region Methods
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

        transform.localPosition += moveInput;
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
        Vector3 shootTo = GetCursorPoint(out hitEnemy, out RaycastHit hitInfo);

        if (Input.GetMouseButton(0)) {
            if (weaponHeat.Overheated)
            {
                
                // Play sound for not being able to shoot due to being overheated
            }
            else
            {
                if (hitEnemy)
                {
                    Bullet firedScript = base.Shoot(shootTo);

                    if (firedScript != null)
                    { // bullet actually fired
                        firedScript.SetTarget(hitInfo.collider.gameObject);
                        //firedScript.SetHomingSpeed(bulletHomingSpeed);
                    }
                }
                else
                {
                    base.Shoot(shootTo);
                }
            }

        }


        if (Input.GetMouseButtonDown(1)) {
            base.SpawnSpecialProjectile();
        }
    }

    void ResetHealthBarColor()
    {
        healthMeter.ResetMeterColor();
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


    public override void GiveHealth(float value, bool setHealth = false)
    {
        base.GiveHealth(value, setHealth);

        virtualCam.HealthScreen(1f);
    }


    public override bool TakeDamage(float damage, float setInvincibleTime = 0, bool addPointsIfKilled = true)
    {
        base.TakeDamage(damage, setInvincibleTime, addPointsIfKilled);
        GameManager.instance.ScreenShake(10, 5, 0.8f);
        virtualCam.DamageScreen(0.5f);
       
        return base.TakeDamage(damage, setInvincibleTime);
    }

    public override void OnDeath()
    {
        base.OnDeath();
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
    #endregion
}
