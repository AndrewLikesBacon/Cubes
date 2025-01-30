using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private int maxAngularVelocity = 600;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float horizontal;
    private bool hasDoubleJumped = false;
    private bool movementLocked = false;
    private float dragMultiplier = 1;

    void Update() {

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded()) {

            hasDoubleJumped = false;

        }

        if (Input.GetButtonDown("Jump") && IsGrounded()) {

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

        } else if (Input.GetButtonDown("Jump") && (hasDoubleJumped == false)) {

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            hasDoubleJumped = true;

        }

        rb.angularVelocity -= rb.velocity.x * 180 * Time.deltaTime;

        if (rb.angularVelocity < -maxAngularVelocity) {

            rb.angularVelocity = -maxAngularVelocity;

        } else if (rb.angularVelocity > maxAngularVelocity) {

            rb.angularVelocity = maxAngularVelocity;

        }

        if (rb.velocity.y < -100) {

            rb.velocity = new Vector2(rb.velocity.x, -100);

        }

        if (movementLocked) {

            if (rb.velocity.x < 0) {

                if (Input.GetKey(KeyCode.D)) {

                    dragMultiplier = 1.5f;

                } else if (Input.GetKey(KeyCode.A)) {

                    dragMultiplier = 2/3f;

                }

            } else if (rb.velocity.x > 0) {

                if (Input.GetKey(KeyCode.A)) {

                    dragMultiplier = 1.5f;

                } else if (Input.GetKey(KeyCode.D)) {

                    dragMultiplier = 2/3f;

                }

            } else {

                dragMultiplier = 1;

            }

        }

        if (Math.Abs(rb.velocity.x) < speed && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))) {

            UnlockMovement();

        }
        
        if (movementLocked && IsGrounded()) {

            rb.drag = 4 * dragMultiplier;

        } else if (movementLocked) {

            rb.drag = 0.5f * dragMultiplier;

        }
    }

    private void FixedUpdate() {

        if (!movementLocked) {

            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        }

    }

    private bool IsGrounded() {

        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    public void SetSpeed(float s) {

        speed = s;

    }

    public void SetJumpPower(float p) {

        jumpPower = p;
        
    }

    public void LockMovement() {

        movementLocked = true;

    }

    private void UnlockMovement() {

        rb.drag = 0;
        movementLocked = false;

    }
}
