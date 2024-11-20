using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;  
    public Transform target; // �����
    private float attackRange = 1; 
    private float attackDelay = 1.67f;  // �������� ����� �������
    private float attackTimer = 1.17f;  // ������ ��� �������� ����� �������
    private float moveSpeed = 0.5f;

    private Tower_script towerScript;  // ������ �� ������ ����� ��� ��������� �����

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
            Debug.Log($"���������� �� ����: {distance}, ������ �����: {attackRange}"); // ��� �������� ������� ����� �� �������(����� ������)!!!!!!

            if (distance <= attackRange + 8)
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