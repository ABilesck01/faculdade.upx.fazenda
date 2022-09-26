using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    [Header("Initial values")]
    [SerializeField] private int initialCoins;
    [SerializeField] private int initialPaidCoin;
    [Header("Labels")]
    [SerializeField] private TextMeshProUGUI lblCoins;
    [SerializeField] private TextMeshProUGUI lblPaidCoins;

    private int _currentCoins;
    private int _currentPaidCoin;

    public static PlayerMoney instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _currentCoins = initialCoins;
        _currentPaidCoin = initialPaidCoin;
        lblCoins.text = _currentCoins.ToString();
        lblPaidCoins.text = _currentPaidCoin.ToString();
    }

    public bool SpendCoins(int amount)
    {
        if (_currentCoins < amount)
            return false;

        _currentCoins -= amount;
        lblCoins.text = _currentCoins.ToString();
        return true;
    }
}
