using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
   public static Player Instance;
   public int playerMoney;
   public int maxLifePoints;
   public int currentLifePoints;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   private void Start()
   {
      currentLifePoints = maxLifePoints;
      UpdateLifePoints(0);
   }

   public void UpdateLifePoints(int damage)
   {
      currentLifePoints -= damage;
      if (currentLifePoints <= 0)
      {
         currentLifePoints = 0;
         ActionsController.OnGameOver?.Invoke();
      }

      ActionsController.OnUpdatePlayerHealt?.Invoke();
   }
}
