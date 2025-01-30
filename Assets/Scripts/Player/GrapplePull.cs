using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class GrapplePull : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D playerRB;
    public Movement playerMovement;
    public Grapple grapple;
    public Rigidbody2D grappleRB;
    [SerializeField] private float pullForce;
    [SerializeField] private float lifeSpan;
    [SerializeField] private float maxGrappleDistance;
    private float timeCreated;
    private bool grappling = false;
    private Vector2 directionVector;

    void Start() {
        timeCreated = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            EndGrapple();

        }

        if (collision.CompareTag("Grappleable") || collision.CompareTag("Enemy") || collision.CompareTag("BigRedCube")) {

            grappleRB.velocity = new Vector2(0, 0);
            grappling = true;

        }
    }

    void Update() {

        directionVector = (transform.position - player.position).normalized;

        if ((Time.time - timeCreated) > lifeSpan) {

            EndGrapple();

        }

        var distance = Vector2.Distance(transform.position, player.position);

        if (distance > maxGrappleDistance) {

            EndGrapple();

        }

        if (grappling) {

            playerMovement.enabled = false;

            if (distance < 3) {

                playerRB.AddForce(directionVector * pullForce / 3, ForceMode2D.Force);

            } else {

                playerRB.AddForce(directionVector * pullForce, ForceMode2D.Force);

            }

            playerRB.velocity = new Vector2(0, 0);

        }
    }

    public void EndGrapple() {

        grappling = false;
        playerMovement.enabled = true;
        grapple.SetHasShotGrapple(false);
        Destroy(gameObject);

    }
}
