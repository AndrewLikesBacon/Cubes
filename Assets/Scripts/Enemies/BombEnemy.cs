using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    
    public Transform player;
    public Rigidbody2D playerRb;
    public Movement playerMovement;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    public AudioSource explosionSound;
    [SerializeField] private float attackRange;
    private Vector2 directionVector;
    [SerializeField] private float KnockbackPower;
    
    void Update()
    {
        
        if (Vector2.Distance(player.position, transform.position) < attackRange) {

            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
            explosionSound.Play();

            directionVector = (player.position - transform.position).normalized;
            playerMovement.LockMovement();
            playerRb.AddForce(directionVector * KnockbackPower, ForceMode2D.Impulse);
            
            Destroy(gameObject);

        }
    }
}
