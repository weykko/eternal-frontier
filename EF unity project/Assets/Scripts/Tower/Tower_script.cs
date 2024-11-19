using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Tower_script : MonoBehaviour
{
    public int maxHP = 100;  
    private int currentHP;  

    private void Start()
    {
        currentHP = maxHP;  
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;  
        Debug.Log($"Башня получила {damage} урона. Текущее HP: {currentHP}"); // проверка хп на консоли(убрать)!!!!!

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
