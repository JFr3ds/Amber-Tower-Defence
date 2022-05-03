using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //Singleton
    public static PoolManager Instance;

    [SerializeField] public MyPool[] poolObjects;

    [SerializeField] public float neighbourDistance;

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

    void CreateObject(int quantity, int indexPool)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject go = Instantiate(poolObjects[indexPool].prefObject, poolObjects[indexPool].objectParent).gameObject;
            go.SetActive(false);
            poolObjects[indexPool].objects.Add(go);
            go.name = $"{poolObjects[indexPool].namePool}_{go.transform.GetSiblingIndex()}";
        }
    }

    public GameObject GetObject(int indexPool)
    {
        for (int i = 0; i < poolObjects[indexPool].objects.Count; i++)
        {
            if (!poolObjects[indexPool].objects[i].activeSelf)
            {
                poolObjects[indexPool].objects[i].SetActive(true);
                return  poolObjects[indexPool].objects[i];
            }
        }
        CreateObject(1,indexPool);
        poolObjects[indexPool].objects[poolObjects[indexPool].objects.Count - 1].SetActive(true);
        return poolObjects[indexPool].objects[poolObjects[indexPool].objects.Count - 1];
    }
}

[Serializable]
public class MyPool
{
    public string namePool;
    public GameObject prefObject;
    public Transform objectParent;
    public List<GameObject> objects;
}