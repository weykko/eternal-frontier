using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Attack_anim : StateMachineBehaviour
{
    //private Transform target;      
    //private NavMeshAgent agent;    
    //private Enemy enemyScript;
    //private float attackRange = 10;


    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //    agent = animator.GetComponent<NavMeshAgent>();
    //    enemyScript = animator.GetComponent<Enemy>();

    //    target = enemyScript?.GetTarget();
    //}

    ////override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    ////{
    ////    animator.transform.LookAt(target);
    ////    if (target == null)
    ////    {
    ////        animator.SetBool("IsAttacking", false);
    ////        return;
    ////        Debug.Log("башен нет");
    ////    }

    ////    float distance = Vector3.Distance(animator.transform.position, target.position);

    ////    if (distance < attackRange)
    ////    {
    ////        if (!agent.isStopped)
    ////        {
    ////            agent.isStopped = true;
    ////        }

    ////        animator.SetBool("IsAttacking", true);


    ////        Debug.Log("Атака работает");
    ////    }
    ////    else
    ////    {
    ////        animator.SetBool("IsAttacking", false);
    ////        Debug.Log("Атака не работает");
    ////    }
    ////}
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.transform.LookAt(target);
        
    //}

}
