using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNuke : MonoBehaviour
{
    Player player;
    Collider collider;

    [SerializeField] float damage = 100;
    [SerializeField] float delayUntilTriggerMovement = 5;
    [SerializeField] Vector3 explodeTargetPoint;

    [SerializeField, Space] float rotationSpeed;
    [SerializeField] float moveSpeed;

    [SerializeField, Space] GameObject explosionPrefab;

    Vector3 posLastFrame;

    bool destroy = false;

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

        collider = GetComponent<Collider>();
        if (collider == null) {
            Destroy(gameObject);
        }

        collider.enabled = false;
    }

    void Update()
    {
        if (destroy) {
            Destroy(gameObject);
        }

        if (delayUntilTriggerMovement > 0) {
            delayUntilTriggerMovement -= Time.deltaTime;

            transform.localPosition -= transform.parent.up * moveSpeed * Time.deltaTime;
        } else {
            float targetDistance = (explodeTargetPoint - transform.localPosition).magnitude;
            float moveSpeedThisFrame = moveSpeed * Time.deltaTime;
            if (targetDistance > moveSpeedThisFrame) {
                transform.localPosition += (explodeTargetPoint - transform.localPosition).normalized * moveSpeedThisFrame;
            } else { // target reached, explode
                collider.enabled = true;

                if (explosionPrefab != null) {
                    Instantiate(explosionPrefab);
                }
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((transform.position - posLastFrame).normalized), rotationSpeed * Time.deltaTime);

        posLastFrame = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            return;
        }

        Character otherChara = other.GetComponent<Character>();
        if (otherChara != null) {
            otherChara.TakeDamage(damage);
        }

        destroy = true;
    }
}
