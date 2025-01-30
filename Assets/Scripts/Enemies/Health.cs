using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{

    public MoneyHolder moneyHolder;
    public ShootUltimate shootUltimate;
    public PlayerHealth playerHealth;
    public AudioSource deathSound;
    [SerializeField] private GameObject deathAnimationPrefab;
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private GameObject moneyNumberPrefab;
    [SerializeField] private int health;
    [SerializeField] private int moneyForKill;
    [SerializeField] private int ultChargeForKill;
    [SerializeField] private int healthForKill;
    private bool willTakeLethalDamage = false;
    
    void Update()
    {

        if (health <= 0) {

            moneyHolder.AddMoney(moneyForKill);
            shootUltimate.AddUltCharge(ultChargeForKill);
            playerHealth.AddHealth(healthForKill);
            GameObject moneyNumber = Instantiate(moneyNumberPrefab, transform.position, Quaternion.identity);
            moneyNumber.GetComponent<TextMeshPro>().text = "$" + moneyForKill;
            GameObject explosion = Instantiate(deathAnimationPrefab, transform.position, transform.rotation);
            deathSound.Play();
            Destroy(gameObject);

        }
    }

    public void TakeDamage(int n) {

        health -= n;

        if (health>0) {

            GameObject damageNumber = Instantiate(damageNumberPrefab, transform.position, Quaternion.identity);
            damageNumber.GetComponent<TextMeshPro>().text = n.ToString();

        }
    }

    public void SetUltChargeForKill(int n) {

        ultChargeForKill = n;

    }

    public void CheckForLethalDamage(int d) {

        if (health <= 0) {

            willTakeLethalDamage = false;

        } else if (health - d <= 0) {

            willTakeLethalDamage = true;

        }
        
    }

    public bool GetWillTakeLethalDamage() {

        return willTakeLethalDamage;

    }
}
