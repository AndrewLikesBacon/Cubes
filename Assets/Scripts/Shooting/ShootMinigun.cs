using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootMinigun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private float bulletSpread;
    [SerializeField] private Transform aim;
    [SerializeField] private Transform gunOffset;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private AudioSource cooldownSound;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    private bool fireContinuously;
    private bool fireSingle;
    private float lastFireTime;
    private Transform bulletAngleShift;

    void Update()
    {

        if (fireContinuously || fireSingle) {

            cooldownSound.Stop();

            if (!shotSound.isPlaying) {

                shotSound.Play();

            } else if (!cooldownSound.isPlaying) {

                cooldownSound.Play();

            }

            float timeSinceLastFire = Time.time - lastFireTime;
            
            if (timeSinceLastFire >= timeBetweenShots) {

                FireBullet();
                lastFireTime = Time.time;
                fireSingle = false;

            }

        } else {
            
            shotSound.Stop();

        }

    }

    private void FireBullet() {

        bulletAngleShift = aim;
        bulletAngleShift.Rotate(0, 0, UnityEngine.Random.Range(-bulletSpread, bulletSpread));
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, bulletAngleShift.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.up;
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

    public void EndShotSound() {

        shotSound.Stop();

    }
}
