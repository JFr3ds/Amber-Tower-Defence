using System;
using UnityEngine;

public static class ActionsController
{
    public static Action<Vector3, int, int> OnSetUpUI;
    public static Action<int,int> OnCreateTower;
    public static Action<int> OnGetReward;
    public static Action<int,int,int> OnBuyTower;
    public static Action<int> OnUpdateUI;
    public static Action OnUpdatePlayerHealt;
    public static Action OnLaunchGame;
    public static Action OnEnemyDone;
    public static Action OnGameOver;
}
