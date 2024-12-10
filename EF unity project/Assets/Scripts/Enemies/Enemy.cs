using System.Collections;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Transform target;
    private float attackRange = 1.44f;
    private float attackDelay = 1.67f;
    private float attackTimer = 1.17f;
    private float moveSpeed = 4f;
    private Tower_script towerScript;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint = 0;
    private float pathUpdateInterval = 1.5f;
    private float pathUpdateTimer = 0f;

    private Vector3 lastDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        FindNearestTarget();
        if (target != null)
        {
            towerScript = target.GetComponent<Tower_script>();
        }
        if (seeker != null && target != null)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    private void Update()
    {
        if (target == null)
        {
            FindNearestTarget();
            animator.SetBool("IsAttacking", false);
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        RotateTowards(target.position);

        pathUpdateTimer += Time.deltaTime;
        if (pathUpdateTimer >= pathUpdateInterval && seeker.IsDone())
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            pathUpdateTimer = 0f;
        }

        if (distance <= attackRange)
        {
            animator.SetBool("IsAttacking", true);
            Attack();
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            if (path != null && currentWaypoint < path.vectorPath.Count)
            {
                MoveTowardsTarget();
            }
        }
    }

    private void MoveTowardsTarget()
    {
        if (path != null && target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                return;
            }

            Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            lastDirection = direction;

            Vector3 moveDelta = direction * moveSpeed * Time.deltaTime;
            transform.position += moveDelta;

            if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.1f)
            {
                currentWaypoint++;
            }
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void FindNearestTarget()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTower = null;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTower = tower;
            }
        }

        if (nearestTower != null)
        {
            target = nearestTower.transform;
            towerScript = target.GetComponent<Tower_script>();

            if (towerScript != null)
            {
                attackRange = towerScript.AttackRange;
            }
        }
        else
        {
            target = null;
            towerScript = null;
        }

        if (target != null && seeker != null)
        {
            seeker.StartPath(transform.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            if (towerScript != null)
            {
                towerScript.TakeDamage(10);
            }

            attackTimer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector3 newDirection = Vector3.Cross(lastDirection, Vector3.up).normalized;
            RotateTowards(newDirection);
        }
    }
}
