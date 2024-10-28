using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayshoot : MonoBehaviour
{
    public float damage = 15;

    public void Shoot()
    {
        RaycastHit hit_obj;
        if (Physics.Raycast(transform.position, transform.forward, out hit_obj))
        {
            Debug.DrawRay(transform.position, transform.forward * 100, Color.black);
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
            {
                target.hit(damage);
            }
        }
    }
}
