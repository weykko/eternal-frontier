using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour
{
    public LayerMask enemyLayerMask; // To filter enemies by layer
    private List<Collider> enteredEnemies = new List<Collider>(); // Enemies that have entered the detection range
    private TurretHead turretHead; // Reference to the TurretHead script

    private void Start()
    {
        // Find the TurretHead component in the turret's hierarchy
        turretHead = GetComponentInChildren<TurretHead>();
        // Detect enemies already inside the turret's range at the start
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius, enemyLayerMask);
        foreach (Collider collider in collidersInRange)
        {
            Target tar = collider.GetComponent<Target>();
            if (tar != null)
            {
                Debug.Log("Target found in range at start: " + collider.gameObject.name);
                turretHead.AddTarget(collider.gameObject);  // Add enemies already inside the range to the entered list
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigerred");
        // Check if the object is on the enemy layer
        if ((enemyLayerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            Target target = other.GetComponent<Target>();
            if (target != null)
            {
                Debug.Log("Enemy detected: " + other.gameObject.name);
                enteredEnemies.Add(other);

                // Pass the detected enemy to TurretHead
                if (turretHead != null)
                {
                    turretHead.AddTarget(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the enemy exits the turret's range, remove it from the list
        if (enteredEnemies.Contains(other))
        {
            enteredEnemies.Remove(other);

            // Notify TurretHead that the enemy has exited
            if (turretHead != null)
            {
                turretHead.RemoveTarget(other.gameObject);
            }
        }
    }
}
