using System;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    

    private void Start()
    {
        ActionsController.OnGetReward += GetReward;
        ActionsController.OnBuyTower += BuyTower;
        UpdateUI();
    }

    private void OnDestroy()
    {
        ActionsController.OnGetReward -= GetReward;
        ActionsController.OnBuyTower -= BuyTower;
    }

    public void GetReward(int reward)
    {
        Player.Instance.playerMoney += reward;
        UpdateUI();
    }

    public void BuyTower(int cost, int towerIndex, int poolTower)
    {
        if (Player.Instance.playerMoney >= cost)
        {
            Player.Instance.playerMoney -= cost;
            ActionsController.OnCreateTower?.Invoke(towerIndex, poolTower);
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        ActionsController.OnUpdateUI?.Invoke(Player.Instance.playerMoney);
    }
}