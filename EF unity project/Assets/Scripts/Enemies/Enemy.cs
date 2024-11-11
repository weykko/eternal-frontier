using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator animator;  // ������ �� ��������� Animator
    private NavMeshAgent agent;  // ������ �� NavMeshAgent ��� ��������
    public Transform target;    // ���� (��������, �����)
    public float attackRange = 1f;  // ������ ��� ������ �����
    public float attackDelay = 1f;  // �������� ����� �������
    private float attackTimer = 0f;  // ������ ��� �������� ����� �������

    private void Start()
    {
        animator = GetComponent<Animator>();  // �������� ��������� Animator
        agent = GetComponent<NavMeshAgent>();  // �������� ��������� NavMeshAgent
        agent.stoppingDistance = attackRange;  // ������������� ���������� ��� ��������� ����� �����

        // ����������� Idle ��� ������
        animator.Play("Idle");
    }

    private void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();  // ������� ����� � ����
        }
    }

    private void MoveTowardsTarget()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)  // ���� ���� �� ������ ����
        {
            agent.SetDestination(target.position);  // ������� ����� � ����

            // �������� �������� Idle ��� ����������� ��������
            animator.Play("Idle");
        }
        else
        {
            Attack();  // ���� � ������� �����, �������� ���������
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            animator.Play("Attack");  // ����������� �������� �����

            // ������ ����� (��������, ���������� HP �����)
            // towerScript.TakeDamage(attackDamage);

            attackTimer = 0f;  // ����� �������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            target = other.transform;  // ������������� ���� ��� ��������� � ������ �����
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            target = null;  // ���������� ����, ���� ���� �������� ������ �����
            animator.Play("Idle");  // ������������� �������� �� Idle
        }
    }
}
