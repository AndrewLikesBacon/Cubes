using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int damage;
    private bool hasHitEnemy;
    private bool letBulletPenetrate;
    
    private void OnTriggerEnter2D(Collider2D collision) {

        if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet")) {

            if (collision.GetComponent<Health>() && !hasHitEnemy) {

                if (collision.GetComponent<Health>().GetWillTakeLethalDamage()) {
                    
                    letBulletPenetrate = true;
                    hasHitEnemy = false;

                } else {

                    hasHitEnemy = true;
                    collision.GetComponent<Health>().CheckForLethalDamage(damage);
                    collision.GetComponent<Health>().TakeDamage(damage);
                    GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                    Destroy(gameObject);

                }
                
            }

            if (!letBulletPenetrate) {
                
                GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
                Destroy(gameObject);

            }
        }
    }
}
