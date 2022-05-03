using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletParabolic : Bullet
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask lm;
    [SerializeField] private float predictedUnits;

    [SerializeField] private float maxHeight;
    [SerializeField] private float gravity = -9.8f;
    private Rigidbody rb;

    public override void Seek(Transform _target)
    {
        base.Seek(_target);
        OnSatrtLauch();   
    }
    
    void OnSatrtLauch()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.useGravity = true;
        rb.velocity = CalculateForce();
    }

    Vector3 CalculateForce()
    {
        Vector3 targetDirection = (target.transform.position + target.forward * predictedUnits) - transform.position;
        float velocityX;
        float velocityY;
        float velocityZ;

        velocityY = Mathf.Sqrt(-2 * gravity * maxHeight);

        velocityX = targetDirection.x / ((-velocityY / gravity) + Mathf.Sqrt(2 * (targetDirection.y - maxHeight) / gravity));
        velocityZ = targetDirection.z / ((-velocityY / gravity) + Mathf.Sqrt(2 * (targetDirection.y - maxHeight) / gravity));
        
        return new Vector3(velocityX, velocityY, velocityZ);
    }

    private void OnCollisionEnter(Collision other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, lm);
        foreach (Collider collider in colliders)
        {
            
            if (collider.GetComponentInParent<IDamageable>() != null)
            {
                float distance = Vector3.Distance(collider.transform.position, transform.position);
                float damage = distance / radius;
                damage = damage - 1f;
                if (damage < 0)
                {
                    damage *= -1;
                }

                damage *= maxDamage;
                collider.GetComponentInParent<IDamageable>().GetDamage(damage);
            }
        }
        gameObject.SetActive(false);
    }
}
