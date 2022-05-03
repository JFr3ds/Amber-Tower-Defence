using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float maxDamage;

    public virtual void Seek(Transform _target)
    {
        target = _target;
        if (target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}