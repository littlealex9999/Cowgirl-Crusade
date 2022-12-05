using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNuke : MonoBehaviour
{
    Player player;

    [SerializeField] float damage = 100;
    [SerializeField] float delayUntilTriggerMovement = 5;
    [SerializeField] Vector3 explodeTargetPoint;
    [SerializeField] float explosionSize = 50;

    [SerializeField, Space] float rotationSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField, Space] GameObject explosionPrefab;

    Vector3 posLastFrame;

    void Start()
    {
        if (transform.parent != null) {
            player = transform.parent.GetComponent<Player>();
            if (player == null) {
                Destroy(gameObject);
            }

            Vector3 offset = transform.localPosition + transform.parent.localPosition;

            transform.parent = player.transform.parent;
            transform.localPosition = offset;

            transform.rotation = player.transform.rotation;
        }
    }

    void Update()
    {
        if (delayUntilTriggerMovement > 0) {
            delayUntilTriggerMovement -= Time.deltaTime;

            transform.localPosition -= transform.parent.up * moveSpeed * Time.deltaTime;
        } else {
            float targetDistance = (explodeTargetPoint - transform.localPosition).magnitude;
            float moveSpeedThisFrame = moveSpeed * Time.deltaTime;
            if (targetDistance > moveSpeedThisFrame) {
                transform.localPosition += (explodeTargetPoint - transform.localPosition).normalized * moveSpeedThisFrame;
            } else { // target reached, explode
                Collider[] objectsHit = Physics.OverlapSphere(transform.position, explosionSize);
                for (int i = 0; i < objectsHit.Length; ++i) {
                    if (objectsHit[i].tag != "Player") {
                        Character hitChara = objectsHit[i].GetComponent<Character>();

                        if (hitChara != null) {
                            hitChara.TakeDamage(damage);
                        }
                    }
                }

                if (explosionPrefab != null) {
                    Instantiate(explosionPrefab);
                }

                Destroy(gameObject);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((transform.position - posLastFrame).normalized), rotationSpeed * Time.deltaTime);

        posLastFrame = transform.position;
    }
}
