using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float radius;

    void Start() {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hitCollider in hitColliders) {

            if (hitCollider.GetComponent<Health>()) {

                hitCollider.GetComponent<Health>().SetUltChargeForKill(0);
                hitCollider.GetComponent<Health>().TakeDamage(damage);

            }

        }
    }
}
