using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    public AudioSource explosionSound;
    
    private void OnTriggerEnter2D(Collider2D collision) {

        if (!collision.CompareTag("Player")) {
            
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
            explosionSound.Play();
            Destroy(gameObject);

        }
    }
}
