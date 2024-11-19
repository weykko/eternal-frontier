using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class idle_script : StateMachineBehaviour
{
    Transform target;
    public float attackRange = 15;
    NavMeshAgent agent;

    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Tower").transform;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(target);
        float distance = Vector3.Distance(animator.transform.position, target.position);
        if (distance < attackRange)
        {
            
            animator.SetBool("IsAttacking", true);

            
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    
}
