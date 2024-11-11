using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower_script : MonoBehaviour
{
    public float maxHP = 100f;  // Максимальное количество здоровья
    public float currentHP;  // Текущее количество здоровья
    public float attackDamage = 10f;  // Урон, который наносит башня
    public float attackRate = 1f;  // Частота атак (в секундах)
    public Collider towerCollider;  // Ссылка на коллайдер башни (для проверки на попадание врага)

    private bool isAttacking = false;  // Флаг для проверки, атакует ли башня

    private void Start()
    {
        currentHP = maxHP;  // Изначально здоровье башни равно максимальному
    }



    // Метод для получения урона башней
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            DestroyTower();
        }
    }

    // Метод для разрушения башни
    private void DestroyTower()
    {
        // Можно добавить эффекты разрушения, анимации, звуки и т. д.
        Debug.Log("Tower Destroyed!");
        Destroy(gameObject);  // Уничтожаем объект башни
    }
}