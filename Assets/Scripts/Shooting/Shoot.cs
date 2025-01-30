using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private Transform gunOffset;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    private bool fireContinuously;
    private bool fireSingle;
    private float lastFireTime;

    void Update()
    {

        if (fireContinuously || fireSingle) {

            float timeSinceLastFire = Time.time - lastFireTime;
            
            if (timeSinceLastFire >= timeBetweenShots) {

                FireBullet();

                lastFireTime = Time.time;
                fireSingle = false;

            }
        }
    }

    private void FireBullet() {

        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.up;
        shotSound.Play();
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

    }

    private void OnFire(InputValue inputValue) {

        fireContinuously = inputValue.isPressed;

        if (inputValue.isPressed) {
            
            fireSingle = true;

        }

    }

    public void SetTimeBetweenShots(float t) {

        timeBetweenShots = t;

    }

    public void UpdateLastFireTime() {

        lastFireTime = Time.time;

    }

    public void SetFireSingle(bool b) {

        fireSingle = b;
        
    }
}
