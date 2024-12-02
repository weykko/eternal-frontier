using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class idle_script : StateMachineBehaviour
{
    Transform target;
    public float attackRange = 11;
    NavMeshAgent agent;
    
    private Enemy enemyScript;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyScript = animator.GetComponent<Enemy>();

        target = enemyScript?.GetTarget();

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        target = enemyScript?.GetTarget();
        float distance = Vector3.Distance(animator.transform.position, target.position);
        Debug.Log($"Расстояние до цели: {distance}");
        if (distance < attackRange)
        {

            animator.SetBool("IsAttacking", true);
            Debug.Log($"работает");


        }

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }



}
