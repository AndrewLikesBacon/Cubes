using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ShootShotgun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private int pelletCount;
    [SerializeField] private float bulletSpread;
    [SerializeField] private Transform aim;
    [SerializeField] private Transform gunOffset;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    private bool fireContinuously;
    private bool fireSingle;
    private float lastFireTime;
    private Transform pellet;

    void Update()
    {

        if (fireContinuously || fireSingle) {

            float timeSinceLastFire = Time.time - lastFireTime;
            
            if (timeSinceLastFire >= timeBetweenShots) {

                FireShotgun();

                lastFireTime = Time.time;
                fireSingle = false;

            }
        }
    }

    private void FireShotgun() {

        pellet = aim;

        for (int i=0; i<=pelletCount/2; i++) {

            pellet.Rotate(0, 0, -bulletSpread);

        }

        for (int i=0; i<pelletCount; i++) {

            pellet.Rotate(0, 0, bulletSpread);
            FirePellet();

        }

        shotSound.Play();
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);

    }

    private void FirePellet() {

        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, pellet.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.up;

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
