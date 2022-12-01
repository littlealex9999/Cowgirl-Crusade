using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float baseDamage = 5;
    [SerializeField] private float baseSpeed = 5;

    float shootDistance = 10;

    [SerializeField] float width = 0.4f;


    [SerializeField] Transform origin;
    Transform endpoint;
    LineRenderer laserLine;
    Camera mainCamera;

    float dmg;
    float spd;

    Character.TEAMS team;

    GameObject homingTarget;
    float homingRotationSpeed = 5;

    private void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.startWidth = width;
        laserLine.endWidth = width;
    }

    void Update()
    {
        Shoot();
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.gameObject.GetComponent<Character>();

        if (character != null && character.getTeam != team)
        {
            character.TakeDamage(dmg); 
            
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            laserLine.enabled = true;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //LayerMask.NameToLayer("RayHitLayer")
            if (Physics.Raycast(ray, out hitInfo, -mainCamera.transform.localPosition.z + shootDistance, 1 << 6))
            {
               
            }
            else
            {
                Vector3 point = Input.mousePosition;
                point.z = -mainCamera.transform.localPosition.z + shootDistance;
                endpoint.position = point;

                //base.Shoot(mainCamera.ScreenToWorldPoint(point));
            }

            Debug.DrawRay(ray.origin, ray.direction, Color.blue);
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * (-mainCamera.transform.localPosition.z + shootDistance), Color.cyan);
        }
        else
        {
            laserLine.enabled = false;
        }

        if (laserLine.enabled)
        {
            laserLine.SetPosition(0, origin.position);
            laserLine.SetPosition(1, endpoint.position);
        }
        

    }
   


}
