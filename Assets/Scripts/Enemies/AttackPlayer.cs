using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float attackForce;
    [SerializeField] private float attackDistance;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float movementLockoutTime;
    private Vector2 directionVector;
    private float distance;
    private float lastAttackTime;

    void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position); 
        directionVector = (player.position - transform.position).normalized;

        if (distance < attackDistance && Time.time - timeBetweenAttacks > lastAttackTime) {

            GetComponent<Follow>().LockMovement(movementLockoutTime);

            rb.AddForce(directionVector * attackForce, ForceMode2D.Impulse);
            lastAttackTime = Time.time;

        }
    }
}
