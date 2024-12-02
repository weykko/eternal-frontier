using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public Transform target; 

    private float attackRange = 0.5f;
    private float attackDelay = 1.67f; // Задержка между атаками
    private float attackTimer = 1.17f; // Таймер для задержки между атаками
    private float moveSpeed = 0.5f;

    private Tower_script towerScript; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        FindNearestTarget(); 
        if (target != null)
        {
            towerScript = target.GetComponent<Tower_script>();
        }
    }

    private void Update()
    {
        if (target == null)
        {
            FindNearestTarget();
            animator.SetBool("IsAttacking", false); // Возвращаемся в состояние Idle
            return;
        }

        RotateTowardsTarget();
        float distance = Vector3.Distance(transform.position, target.position);
        //Debug.Log($"Расстояние до цели: {distance}"); // потом убрать

        if (distance <= attackRange + 7)
        {
            Attack();
            animator.SetBool("IsAttacking", true);

        }
        else 
        {
            MoveTowardsTarget();
            
        }
    }

    private void MoveTowardsTarget()
    {
        animator.SetBool("IsAttacking", false);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

    private void FindNearestTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTower = null;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTower = tower;
            }
        }

        if (nearestTower != null)
        {
            target = nearestTower.transform;
            towerScript = target.GetComponent<Tower_script>(); // тут обновляется ссылка
        }
        else
        {
            target = null;
            towerScript = null; // Удаляем ссылку, если нет целей
        }
    }

    //public void ClearTarget()
    //{
    //    target = null;
    //    towerScript = null; 
    //}

    //public Transform GetTarget()
    //{
    //    return target;
    //}

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
    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0; 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); 
        }
    }

    public Transform GetTarget() 
    {
        return target;
    }

}
