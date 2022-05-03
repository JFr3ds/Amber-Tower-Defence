using UnityEngine;

public class TowerCreator : MonoBehaviour
{
    public GameObject myTower;
    public int towerIndex;
    public int indexScreen = 0;

    private void Awake()
    {
        ActionsController.OnCreateTower += OnCreateTower;
    }

    private void OnDestroy()
    {
        ActionsController.OnCreateTower -= OnCreateTower;
    }

    void OnCreateTower(int index, int indexTowerPool)
    {
        if (index == transform.GetSiblingIndex() && !myTower)
        {
            towerIndex = indexTowerPool;
            indexScreen = 1;
            myTower = PoolManager.Instance.GetObject(indexTowerPool);
            myTower.transform.position = transform.position;
        }
    }
}