using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UltimateChargeUpgrade : MonoBehaviour
{

    [SerializeField] private GameObject aim;
    [SerializeField] private int ultChargeRequirement;
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
        aim.GetComponent<ShootUltimate>().SetUltChargeRequirement(ultChargeRequirement);
        upgradeLevel++;
        cost *= 2;
        ultChargeRequirement = (int)(ultChargeRequirement / 1.1);

        if (upgradeLevel == levelCap) {

            LevelText.SetText("Ultimate Charge Speed Level: " + upgradeLevel);

        } else {

            LevelText.SetText("Ultimate Charge Speed Level: " + upgradeLevel + "\nCost: $" + cost);

        }

        hasUpgraded = true;

    }
}
