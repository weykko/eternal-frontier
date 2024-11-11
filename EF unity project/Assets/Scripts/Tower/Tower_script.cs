using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower_script : MonoBehaviour
{
    public float maxHP = 100f;  // ������������ ���������� ��������
    public float currentHP;  // ������� ���������� ��������
    public float attackDamage = 10f;  // ����, ������� ������� �����
    public float attackRate = 1f;  // ������� ���� (� ��������)
    public Collider towerCollider;  // ������ �� ��������� ����� (��� �������� �� ��������� �����)

    private bool isAttacking = false;  // ���� ��� ��������, ������� �� �����

    private void Start()
    {
        currentHP = maxHP;  // ���������� �������� ����� ����� �������������
    }



    // ����� ��� ��������� ����� ������
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            DestroyTower();
        }
    }

    // ����� ��� ���������� �����
    private void DestroyTower()
    {
        // ����� �������� ������� ����������, ��������, ����� � �. �.
        Debug.Log("Tower Destroyed!");
        Destroy(gameObject);  // ���������� ������ �����
    }
}