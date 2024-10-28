using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Turret : MonoBehaviour {
    private Rayshoot shoot;
    public float fireRate = 4;
    private float rotationSpeed = 10f;

    private float nextTimeToFire = 0f;
    private List<Collider> entered = new();
    private GameObject currTarget = null;

    private void Start()
    {
        shoot = GetComponent<Rayshoot>();

        // Detect enemies already inside the turret's range at the start
        var collidersInRange = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);
        foreach (var collider in collidersInRange)
        {
            Debug.Log("Detected" + collider.name);
            var tar = collider.GetComponent<Target>();
            if (tar != null) entered.Add(collider); // Add enemies already inside the range to the entered list
        }
    }

    private void Update()
    {
        if (!currTarget) currTarget = SelectNextTarget();
        LookAt(currTarget);
        if (Time.time >= nextTimeToFire)
            if (currTarget)
            {
                nextTimeToFire += 1f / fireRate;
                shoot.Shoot();
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        var tar = other.GetComponent<Target>();
        if (tar)
        {
            Debug.Log("entered");
            entered.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entered.Contains(other)) entered.Remove(other);

        if (other.gameObject == currTarget) currTarget = SelectNextTarget();
    }

    private GameObject SelectNextTarget()
    {
        if (entered.Count > 0)
        {
            entered.Sort(Comparison);
            var target = entered.First();
            entered.Remove(target);
            return target.gameObject;
        }

        return null;
    }

    private int Comparison(Collider x, Collider y)
    {
        if (x&& y)
        {
            var x_meDist = Vector3.Distance(x.transform.position, transform.position);
            var y_meDist = Vector3.Distance(y.transform.position, transform.position);
            return x_meDist.CompareTo(y_meDist);
        }

        return 0;
    }

    private void LookAt(GameObject tar)
    {
        if (!currTarget) currTarget = SelectNextTarget();

        if (!currTarget) return;
        var direction = tar.transform.position - transform.position;
        var lookRotation = Quaternion.LookRotation(direction);
        var targetRotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime)
            .eulerAngles;
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}
