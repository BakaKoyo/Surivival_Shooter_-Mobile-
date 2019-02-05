using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{

    protected override void OnDeath()
    {

        base.OnDeath();

        Destroy(gameObject);

    }


    private void FixedUpdate()
    {
        MoveToPlayer();
    }


    void MoveToPlayer()
    {
        /* 
        This gives us a value of -1 to 1 for direction
        Formula: Target - self
        */
        if (GameManager.Instance.Player != null)
        {
            Vector3 dir = (GameManager.Instance.Player.transform.position - transform.position).normalized;
            Move(dir);
        }
    }

}
