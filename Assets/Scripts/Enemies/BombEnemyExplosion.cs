using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemyExplosion : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool hasTakenDamage = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player") && !hasTakenDamage) {
            
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            hasTakenDamage = true;
            GetComponent<CircleCollider2D>().enabled = false;

        }
    }
}
