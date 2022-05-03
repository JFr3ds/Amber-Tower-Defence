using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(LineRenderer))]
public class TowerLaser : Tower
{
    [SerializeField] private float damageOverTime;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private IDamageable damageable;

    public override void Update()
    {
        base.Update();
        if (!TargetInRange())
        {
            if (lr.enabled)
            {
                lr.enabled = false;
            }

            return;
        }

        RotateTower();
        UseLaser();
    }

    public override void UpdateTarget()
    {
        base.UpdateTarget();
        if (target != null)
        {
            damageable = target.GetComponent<IDamageable>();
        }
    }

    void UseLaser()
    {
        if (!lr.enabled)
        {
            lr.enabled = true;
        }

        lr.SetPosition(0, spawnAmmo.position);
        lr.SetPosition(1, target.position);


        damageable.GetDamage(damageOverTime * Time.deltaTime);
    }
}

