using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Attack_anim : StateMachineBehaviour
{
    Transform target;
    public float attackRange = 15;
    private bool isAttacking = false;
    NavMeshAgent agent;
    // Флаг для отслеживания состояния атаки

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Tower").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(target);
        float distance = Vector3.Distance(animator.transform.position, target.position);

        if (distance < attackRange)
        {
            //agent.isStopped = true;
            animator.SetBool("IsAttacking", true);
           
           
        }
        else
        {
            //agent.isStopped = false;
            animator.SetBool("IsAttacking", false);
         
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = false; 
    }
}

