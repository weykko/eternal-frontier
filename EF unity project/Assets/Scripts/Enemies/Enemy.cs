using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;  // Ссылка на компонент Animator
    private NavMeshAgent agent;  // Ссылка на NavMeshAgent для движения
    public Transform target;    // Цель (например, башня)
    public float attackRange = 1f;  // Радиус для начала атаки
    public float attackDelay = 1f;  // Задержка между атаками
    private float attackTimer = 0f;  // Таймер для задержки между атаками

    private void Start()
    {
        animator = GetComponent<Animator>();  // Получаем компонент Animator
        agent = GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent
        agent.stoppingDistance = attackRange;  // Устанавливаем расстояние для остановки перед целью

        // Проигрываем Idle для начала
        animator.Play("Idle");
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();  // Двигаем врага к цели
        }
    }

    private void MoveTowardsTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)  // Если враг не достиг цели
        {
            agent.SetDestination(target.position);  // Двигаем врага к цели

            // Включаем анимацию Idle для отображения движения
            animator.Play("Idle");
        }
        else
        {
            Attack();  // Если в радиусе атаки, начинаем атаковать
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            animator.Play("Attack");  // Проигрываем анимацию атаки

            // Логика атаки (например, уменьшение HP башни)
            // towerScript.TakeDamage(attackDamage);

            attackTimer = 0f;  // Сброс таймера
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            target = other.transform;  // Устанавливаем цель при попадании в радиус башни
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            target = null;  // Сбрасываем цель, если враг покидает радиус башни
            animator.Play("Idle");  // Останавливаем анимацию на Idle
        }
    }
}
