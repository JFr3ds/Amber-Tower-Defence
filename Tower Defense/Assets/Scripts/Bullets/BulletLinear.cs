using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BulletLinear : Bullet
{
    [SerializeField] private float movementSpeed;
    public override void Seek(Transform _target)
    {
        base.Seek(_target);
        transform.rotation = ToolBox.GetDesireRotation(target.position, transform.position);
    }

    private void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = movementSpeed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (target.gameObject.activeSelf)
        {
            target.GetComponent<IDamageable>().GetDamage(maxDamage);
        }
        gameObject.SetActive(false);
    }
}