using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   public int damage;
   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("장애물 발견");
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakePhysicalDamage(damage) ;
        }
    }
    
}
