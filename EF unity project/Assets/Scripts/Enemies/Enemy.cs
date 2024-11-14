using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float moveSpeed = 0.5f;

    private void Start()
    {
        Debug.Log("S");
        animator = GetComponent<Animator>();  // Получаем компонент Animator
        agent = GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent
        agent.stoppingDistance = attackRange;  // Устанавливаем расстояние для остановки перед целью
        agent.speed = moveSpeed;

        // Проигрываем Idle для начала

    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
        }
        //if (distance < attackRange)
        //{
        //    EnemyAnimatorController.SetBool("IsAttacking", true);
        //}
        

        //if (target != null)
        //{
        //    MoveTowardsTarget();  // Двигаем врага к цели
        //}
    }

    private void MoveTowardsTarget()
    {
        /*
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)  // Если враг не достиг цели
        {
            agent.SetDestination(target.position);  // Двигаем врага к цели

            // Включаем анимацию Idle для отображения движения
            
        }
        else
        {
            Attack();  // Если в радиусе атаки, начинаем атаковать
            agent.Stop(true);
        }*/
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
              // Проигрываем анимацию атаки

            // Логика атаки (например, уменьшение HP башни)
            // towerScript.TakeDamage(attackDamage);

            attackTimer = 0f;  // Сброс таймера
        }
    }

    //[SerializeField] private Animator EnemyAnimatorController;

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("S");
    //    if (other.CompareTag("Tower"))
    //    {
    //        //target = other.transform;  // Устанавливаем цель при попадании в радиус башни
    //        EnemyAnimatorController.SetBool("IsAttacking", true);
    //        Debug.Log("Enter");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Tower"))
    //    {
    //        Debug.Log("E");
    //        target = null;  // Сбрасываем цель, если враг покидает радиус башни
    //        EnemyAnimatorController.SetBool("IsAttacking", false);
    //    }
    //}
}
