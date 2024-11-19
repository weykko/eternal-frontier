using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;  
    public Transform target; // башня
    public float attackRange = 12; 
    public float attackDelay = 1f;  // Задержка между атаками
    private float attackTimer = 0f;  // Таймер для задержки между атаками
    public float moveSpeed = 1f;

    private Tower_script towerScript;  // Ссылка на скрипт башни для нанесения урона

    private void Start()
    {
        animator = GetComponent<Animator>();  

        if (target != null)
        {
            towerScript = target.GetComponent<Tower_script>(); 
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            Debug.Log($"Расстояние до цели: {distance}, радиус атаки: {attackRange}"); // для проверки условия атаки на консоли(потом убрать)!!!!!!

            if (distance <= attackRange)
            {
                Attack();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
            }
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
           

            if (towerScript != null)
            {
                towerScript.TakeDamage(10);
            }

            attackTimer = 0f; 
        }
    }
}