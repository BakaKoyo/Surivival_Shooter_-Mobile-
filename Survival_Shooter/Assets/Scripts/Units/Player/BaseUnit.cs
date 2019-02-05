using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour, IAttackable
{

    [SerializeField]
    protected int Max_HP = 1;

    [SerializeField]
    protected float speed = 3.5f;

    [SerializeField]
    protected int Damage = 1;

    Rigidbody rb;

    int Current_HP;

    /* Let's any other class see if we are dead, but deny acess to edit the bool directly. */
    public bool IsDead { get; private set; }


    private void Awake()
    {
        OnAwake();
        rb = GetComponent<Rigidbody>();
    }


    protected virtual void OnAwake()
    {
        Current_HP = Max_HP;
    }

    public void OnHit(int _damage)
    {
        Current_HP -= _damage;

        /* Check if we are not dead, 
           to not call this function again incase the object is not destroyed */
        if (Current_HP <= 0 && IsDead == false) OnDeath();
    }


    protected virtual void OnDeath()
    {      
        IsDead = true;
    }

    protected void Move(Vector3 dir)
    {
        if (IsDead == false)
        {
            dir *= speed;
            rb.velocity = new Vector3(dir.x, rb.velocity.y, dir.z);
        }
    }

}
