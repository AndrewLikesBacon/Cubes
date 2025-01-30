using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootSniper : MonoBehaviour
{
    [SerializeField] private float timeBetweenShots = 0.4f;
    [SerializeField] private Transform gunOffset;
    private bool fireContinuously;
    private bool fireSingle;
    private float lastFireTime;
    [SerializeField] private AudioSource shotSound;
    [SerializeField] private float shakeIntensity;
    [SerializeField] private float shakeTime;
    [SerializeField] private int damage;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float lineDisplayTime;

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

        if (Time.time - lastFireTime > lineDisplayTime) {

            lineRenderer.enabled = false;

        }
    }

    private void FireBullet() {

        RaycastHit2D hitInfo = Physics2D.Raycast(gunOffset.position, transform.up);

        if(hitInfo) {

            if (hitInfo.transform.GetComponent<Health>()) {

                hitInfo.transform.GetComponent<Health>().TakeDamage(damage);

            }

            lineRenderer.SetPosition(0, gunOffset.position);
            lineRenderer.SetPosition(1, hitInfo.point);

        } else {

            lineRenderer.SetPosition(0, gunOffset.position);
            lineRenderer.SetPosition(1, gunOffset.position + gunOffset.up * 50);

        }

        shotSound.Play();
        CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeTime);
        lineRenderer.enabled = true;

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

    public void DisableLineRenderer() {

        lineRenderer.enabled = false;

    }
}
