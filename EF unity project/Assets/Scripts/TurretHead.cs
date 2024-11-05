using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurretHead : MonoBehaviour {
    public Transform rotationPart;
    public Transform aimPart;
    public float rotationSpeed = 10f; // Rotation speed of the turret head
    private readonly List<GameObject> targets = new(); // List of targets detected by TurretBase
    private GameObject currentTarget; // The current target to aim at
    private readonly Vector3 rotationOffset = new(0, 0, 0);
    private Rayshoot shooter;
    private readonly float fireRate = 4f;
    private float nextTimeToFire;
    
    private void Start()
    {
        shooter = GetComponentInChildren<Rayshoot>();
    }

    private void Update()
    {
        // Select the next available target if there's no current one
        if (!currentTarget && targets.Count > 0) SelectNextTarget();

        // Rotate the turret head towards the current target
        if (currentTarget)
        {
            RotateTowards(currentTarget);
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire += 1f / fireRate;
                shooter.Shoot();
            }
        }
    }

    // Method to add a new target from TurretBase
    public void AddTarget(GameObject target)
    {
        if (!targets.Contains(target))
        {
            targets.Add(target);
            Debug.Log("Target added to turret: " + target.name);
        }
    }

    // Method to remove a target (e.g., if it exits the turret's range)
    public void RemoveTarget(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
            // If the current target is removed, choose a new one
            if (currentTarget == target) SelectNextTarget();
        }
    }

    // Method to rotate the turret head towards the target
    private void RotateTowards(GameObject target)
    {
        var direction = target.transform.position - aimPart.position;
        var targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= Quaternion.Euler(rotationOffset);

        rotationPart.rotation = Quaternion.Slerp(rotationPart.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Method to select the next target from the list
    private void SelectNextTarget()
    {
        if (targets.Count > 0)
            foreach (var target in targets)
                if (!target)
                {
                    RemoveTarget(target);
                }
                else
                {
                    currentTarget = target;
                    break;
                }
        else
            currentTarget = null; // No targets available
    }
}