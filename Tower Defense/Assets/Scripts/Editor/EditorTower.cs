using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerVisualizer))]
public class EditorTower : Editor
{
   private TowerVisualizer towerGroup;
   private void OnEnable()
   {
      towerGroup = target as TowerVisualizer;
   }

   private void OnSceneGUI()
   {
      for (int i = 0; i < towerGroup.transform.childCount; i++)
      {
         Handles.DrawWireDisc(   towerGroup.transform.GetChild(i).position, Vector3.up, towerGroup.transform.GetChild(i).GetComponent<Tower>().rangeOfAttack);
      }
      
   }
}
