using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float moveSpeed = 0.5f;

    private void Start()
    {
        Debug.Log("S");
        animator = GetComponent<Animator>();  // �������� ��������� Animator
        agent = GetComponent<NavMeshAgent>();  // �������� ��������� NavMeshAgent
        agent.stoppingDistance = attackRange;  // ������������� ���������� ��� ��������� ����� �����
        agent.speed = moveSpeed;

        // ����������� Idle ��� ������

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
        //    MoveTowardsTarget();  // ������� ����� � ����
        //}
    }

    private void MoveTowardsTarget()
    {
        /*
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)  // ���� ���� �� ������ ����
        {
            agent.SetDestination(target.position);  // ������� ����� � ����

            // �������� �������� Idle ��� ����������� ��������
            
        }
        else
        {
            Attack();  // ���� � ������� �����, �������� ���������
            agent.Stop(true);
        }*/
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
              // ����������� �������� �����

            // ������ ����� (��������, ���������� HP �����)
            // towerScript.TakeDamage(attackDamage);

            attackTimer = 0f;  // ����� �������
        }
    }

    //[SerializeField] private Animator EnemyAnimatorController;

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("S");
    //    if (other.CompareTag("Tower"))
    //    {
    //        //target = other.transform;  // ������������� ���� ��� ��������� � ������ �����
    //        EnemyAnimatorController.SetBool("IsAttacking", true);
    //        Debug.Log("Enter");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Tower"))
    //    {
    //        Debug.Log("E");
    //        target = null;  // ���������� ����, ���� ���� �������� ������ �����
    //        EnemyAnimatorController.SetBool("IsAttacking", false);
    //    }
    //}
}
