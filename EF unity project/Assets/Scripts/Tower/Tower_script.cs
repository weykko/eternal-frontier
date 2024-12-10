using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_script : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public float AttackRange = 2.0f; // Радиус атаки башни

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
