using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : Tower
{
    [SerializeField] private float fireRate;
    [SerializeField] private int bulletPoolIndex;
    private float refFireRate;

    public override void Update()
    {
        base.Update();

        if (!TargetInRange())
        {
            return;
        }

        RotateTower();

        refFireRate += Time.deltaTime;
        if (refFireRate >= fireRate)
        {
            refFireRate = 0;
            if (TargetInRange())
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetObject(bulletPoolIndex);
        bullet.transform.position = spawnAmmo.position;
        bullet.GetComponent<Bullet>().Seek(target);
    }
}