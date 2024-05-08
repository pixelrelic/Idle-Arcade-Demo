using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;
    public int money;
    [SerializeField] TextMeshProUGUI moneyText;
    public event Action moneyCollectedAction;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        money = PlayerPrefs.GetInt("money");
        moneyText.text = money + "";
    }

    public void CollectMoney(int amount)
    {
        money = PlayerPrefs.GetInt("money");
        money += amount;
        PlayerPrefs.SetInt("money", money);
        moneyText.text = money + "";
        moneyCollectedAction?.Invoke();
    }
}
