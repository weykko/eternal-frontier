using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField] private Animator EnemyAnimatorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            EnemyAnimatorController.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            EnemyAnimatorController.SetBool("IsAttacking", false);
        }
    }


}

