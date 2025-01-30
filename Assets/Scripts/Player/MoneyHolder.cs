using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyHolder : MonoBehaviour
{

    private long money = 0;
    [SerializeField] private TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.SetText("$" + money);
    }

    public void AddMoney(int m) {
        money += m;
    }

    public void SubtractMoney(int m) {
        money -= m;
    }

    public long GetMoney() {
        return money;
    }
}
