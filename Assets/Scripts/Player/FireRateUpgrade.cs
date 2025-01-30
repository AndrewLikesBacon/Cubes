using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FireRateUpgrade : MonoBehaviour
{

    [SerializeField] private GameObject aim;
    [SerializeField] private float timeBetweenShots;
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
        aim.GetComponent<Shoot>().SetTimeBetweenShots(timeBetweenShots);
        upgradeLevel++;
        cost *= 2;
        timeBetweenShots /= (float)1.1;

        if (upgradeLevel == levelCap) {

            LevelText.SetText("Fire Rate Level: " + upgradeLevel);

        } else {

            LevelText.SetText("Fire Rate Level: " + upgradeLevel + "\nCost: $" + cost);

        }

        hasUpgraded = true;

    }
}
