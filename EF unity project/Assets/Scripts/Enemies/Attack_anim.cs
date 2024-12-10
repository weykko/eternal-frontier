using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    private float attackRange = 1.44f;
    private float attackDelay = 1.67f;
    private float attackTimer = 1.17f;
    private Tower_script towerScript;
    private Transform target;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy enemyScript = animator.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            target = enemyScript.target;
            towerScript = target?.GetComponent<Tower_script>();
        }
        attackTimer = 0f; 
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (target == null || towerScript == null)
        {
            return;
        }

        float distance = Vector3.Distance(animator.transform.position, target.position);

        if (distance <= attackRange)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                towerScript.TakeDamage(10); 
                attackTimer = 0f;
            }
        }
    }

    // Вызывается при выходе из состояния Attack
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
