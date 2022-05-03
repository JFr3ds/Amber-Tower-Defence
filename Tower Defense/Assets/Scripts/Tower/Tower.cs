using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class Tower : MonoBehaviour
{
   [Header("Tower Movement")]
   [SerializeField] public Transform target;
   [SerializeField] protected float rotationSpeed;
   [SerializeField] protected float searchEnemyTime;
   protected float refSearchTime;

   [Header("Tower Attack")] 
   [SerializeField] protected Transform spawnAmmo;

   [Header("Tower Price")]
   public int towerPrice;
   
   public float rangeOfAttack;

   public virtual void Update()
   {
      refSearchTime += Time.deltaTime;
      if (refSearchTime >= searchEnemyTime)
      {
         refSearchTime = 0;
         UpdateTarget();
      }
   }

   public virtual void UpdateTarget()
   {
      GameObject[] allEnemys;
      float shortestDistance = Mathf.Infinity;
      GameObject nearestEnemy = null;

      allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
      foreach (GameObject go in allEnemys)
      {
         float actualDistance = Vector3.Distance(transform.position, go.transform.position);
         {
            if (go.activeSelf && actualDistance < shortestDistance)
            {
               shortestDistance = actualDistance;
               nearestEnemy = go;
            }
            else
            {
               target = null;
            }
         }
      }

      if (nearestEnemy && shortestDistance <= rangeOfAttack)
      {
         target = nearestEnemy.transform;
      }
   }

   protected bool TargetInRange()
   {
      if (target != null && target.gameObject.activeSelf)
      {
         if (Vector3.Distance(target.position, transform.position) <= rangeOfAttack)
         {
            return true;
         }
         return false;
      }
      else
      {
         return false;
      }
   }

   protected void RotateTower()
   {
      transform.rotation = Quaternion.Slerp(transform.rotation, ToolBox.GetDesireRotation(target.position, transform.position), rotationSpeed * Time.deltaTime);
   }
}
