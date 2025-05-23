using System;

using UnityEngine;

public interface IDamageable
{
    void TakePhysicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamageable
{
    public UICondition uiCondition;
    Condition health{get{ return uiCondition.health;}}
    Condition stamina{get{ return uiCondition.stamina;}}

    public Action onTakeDamage;

    public float GetCurStamina()
    {
        return stamina.curValue;
    }
    

    void Update()
    {
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    public void HealHeath(float amount)
    {
        health.Add(amount);
    }

    public void HealStamina(float amount)
    {
        stamina.Add(amount);
    }
    
    public void Die()
    {
        
    }
    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);    
        onTakeDamage?.Invoke();
    }

    public void UseStamina(float amount)
    {
        stamina.Subtract(amount);
    }
}
