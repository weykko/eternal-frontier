using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float health = 100;
    
    public void hit(float delta)
    {
        health -= delta;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
