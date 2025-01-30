using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Follow : MonoBehaviour
{

    public Transform targetObj;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float followSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float MaxSpeedCapX;
    [SerializeField] private float MaxSpeedCapY;
    [SerializeField] private float minTimeBetweenJumps;
    [SerializeField] private float maxTimeBetweenJumps;
    [SerializeField] private float distanceToDespawn;
    private float distance;
    private float lastJumpTime;
    private float jumpCooldown;
    private Vector2 directionVector;
    private bool movementLocked = false;
    private float movementUnlockTime = 0;

    void Start() {

        lastJumpTime = Time.time;
        jumpCooldown = UnityEngine.Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);

    }

    void Update()
    {

        distance = Vector2.Distance(transform.position, targetObj.transform.position);
        directionVector = (targetObj.position - transform.position).normalized;

        if (distance > distanceToDespawn) {

            Destroy(gameObject);

        }

        if (movementUnlockTime <= Time.time) {

            movementLocked = false;

        }

        if (!movementLocked && distance > 0.8) {

            if (distance < 2) {

                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetObj.position.x, transform.position.y), followSpeed / 2 * distance * Time.deltaTime);
                rb.AddForce(directionVector * followSpeed / 2, ForceMode2D.Force);

            } else {

                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetObj.position.x, transform.position.y), followSpeed * Time.deltaTime);
                rb.AddForce(directionVector * followSpeed, ForceMode2D.Force);

            }

        }

        if (Time.time - jumpCooldown > lastJumpTime) {

            jumpCooldown = UnityEngine.Random.Range(minTimeBetweenJumps, maxTimeBetweenJumps);
            lastJumpTime = Time.time;
            directionVector = (targetObj.position - transform.position).normalized;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            rb.AddForce(directionVector * jumpPower, ForceMode2D.Impulse);
            
        }

        if (rb.velocity.x > MaxSpeedCapX) {

            rb.velocity = new Vector2(MaxSpeedCapX, rb.velocity.y);

        }

        if (rb.velocity.y > MaxSpeedCapY) {

            rb.velocity = new Vector2(rb.velocity.x, MaxSpeedCapY);
            
        }
    }

    public void SetFollowSpeed(float s) {
        
        followSpeed = s;

    }

    public void LockMovement(float t) {

        if (movementUnlockTime < t + Time.time) {

            movementUnlockTime = t + Time.time;

        }

        movementLocked = true;

    }
}
