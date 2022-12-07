using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    Player player;
    [SerializeField] float forwardOffset = 2;
    [SerializeField] float rotationSpeed = 5;

    LineRenderer lr;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
        transform.parent = null;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (player != null) {
            transform.rotation = Quaternion.Slerp(transform.rotation * Quaternion.Euler(-90, 0, 0), 
                                                  Quaternion.LookRotation(player.GetCursorPoint(out bool hitEnemy, out RaycastHit hitInfo) - player.transform.position), 
                                                  rotationSpeed * Time.deltaTime) * Quaternion.Euler(90, 0, 0);
            transform.position = (player.transform.position + player.transform.forward * forwardOffset) + transform.lossyScale.y * transform.up;

            if (lr != null) {
                lr.SetPosition(0, player.transform.position + player.transform.forward * forwardOffset);
                lr.SetPosition(1, transform.position + transform.up * (transform.lossyScale.y));
            }
        } else {
            Destroy(gameObject);
        }
    }

    void SetPlayerObject(Player p)
    {
        player = p;
    }
}
