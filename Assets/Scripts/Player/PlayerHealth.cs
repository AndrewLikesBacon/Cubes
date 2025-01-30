using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageFilter;
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private int damageRecieved;
    [SerializeField] private int damageRecievedBigRedCube;
    [SerializeField] private float damageLeniencyTime;
    [SerializeField] private float redScreenMaxOpacity;
    [SerializeField] private float redScreenFadeTimeToDamageRatio;
    [SerializeField] private float damageShakeIntensity;
    [SerializeField] private float damageShakeTime;
    private float redScreenFadeTime;
    private float lastTimeDamaged = float.MinValue;
    private bool isTouchingEnemy = false;
    private bool isTouchingBigRedCube = false;
    private float timeSinceDamaged;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Enemy")) {

            isTouchingEnemy = true;

        }

        if (collision.CompareTag("BigRedCube"))
        {

            isTouchingBigRedCube = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy")) {

            isTouchingEnemy = false;

        }

        if (collision.CompareTag("BigRedCube")) {

            isTouchingBigRedCube = false;

        }

    }

    private void TakeDamageLenient(int d) {

        if (Time.time - lastTimeDamaged > damageLeniencyTime) {

            TakeDamage(d);
            punchSound.Play();

        }
        
    }

    public void TakeDamage(int d) {

        if (health - d > 0) {

            health -= d;

        } else {

            health = 0;

        }
        
        lastTimeDamaged = Time.time;
        damageFilter.color = new Color(255, 0, 0, 0.25f);
        SetRedScreenFadeTime(d / redScreenFadeTimeToDamageRatio);
        UpdateHealthUI();
        CinemachineShake.Instance.ShakeCamera(damageShakeIntensity, damageShakeTime);

    }

    public void SetMaxHealth(int h) {

        maxHealth = h;
        UpdateHealthUI();

    }
    
    public void SetHealth(int h) {

        health = h;
        UpdateHealthUI();

    }

    public void AddHealth(int h) {

        if (health + h <= maxHealth) {

            health += h;

        } else {

            health = maxHealth;

        }
        
        UpdateHealthUI();

    }

    void Update() {

        if (isTouchingEnemy) {

            TakeDamageLenient(damageRecieved);

        }

        if (isTouchingBigRedCube)
        {

            TakeDamageLenient(damageRecievedBigRedCube);

        }

        timeSinceDamaged = Time.time - lastTimeDamaged;
        damageFilter.color = new Color(255, 0, 0, redScreenMaxOpacity * (1 - (timeSinceDamaged / redScreenFadeTime)));

    }

    private void UpdateHealthUI() {

        healthBar.fillAmount = (float)health / maxHealth;

    }

    private void SetRedScreenFadeTime(float t) {

        redScreenFadeTime = t;

    }
}
