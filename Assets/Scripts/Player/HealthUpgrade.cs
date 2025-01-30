using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int health;
    [SerializeField] private MoneyHolder moneyHolder;
    [SerializeField] private TextMeshPro LevelText;
    [SerializeField] private int cost;
    [SerializeField] private int levelCap;
    private int upgradeLevel = 1;
    private bool hasUpgraded = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player") && hasUpgraded == false) {

            if (moneyHolder.GetMoney() >= cost && upgradeLevel < levelCap) {

                IncreaseUpgradeLevel();
            
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            hasUpgraded = false;

        }

    }

    private void IncreaseUpgradeLevel() {

        moneyHolder.SubtractMoney(cost);
        player.GetComponent<PlayerHealth>().SetMaxHealth(health);
        player.GetComponent<PlayerHealth>().SetHealth(health);
        upgradeLevel++;
        cost *= 2;
        health = (int)(health * 1.1);

        if (upgradeLevel == levelCap) {

            LevelText.SetText("Health Level: " + upgradeLevel);

        } else {

            LevelText.SetText("Health Level: " + upgradeLevel + "\nCost: $" + cost);

        }
        
        hasUpgraded = true;

    }
}
