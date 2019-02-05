using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseUnit
{

    //public void PlayerRotation(Vector2 rot)
    //{
    //    /* Rotate along the axis, in space world */
    //    transform.Rotate(rot, Space.World);
    //}

   
    GyroscopeInputs gyroInput;
    Camera cam;

    VirtualJoystick Joystick;

    protected override void OnAwake()
    {
        base.OnAwake();
        cam = GetComponentInChildren<Camera>();
        gyroInput = GetComponent<GyroscopeInputs>();
        Joystick = FindObjectOfType<VirtualJoystick>();
    }

    private void FixedUpdate()
    {
        /* 
        Null checking for gyroscope if it's enabled and active
        before setting our rotation 
        */
        if (gyroInput != null && gyroInput.Gyro != null)
        {
            cam.transform.localRotation = gyroInput.GetRotation();
        }

        /*
        Null Checking if the Joystick is not null
        */
        if (Joystick != null)
        {
            Vector3 JoyStickdir = new Vector3(Joystick.InputVector.x, 0, Joystick.InputVector.y);
            Move(JoyStickdir);
        }

    }

    void AttackTarget(IAttackable Target)
    {
        Target.OnHit(Damage);
    }


    public void HitCheck(Vector2 pos)
    {

        RaycastHit hitInfo = new RaycastHit();

        /* Pass vector 2 and convert from our camera's screen point */
        Ray ray = GameManager.Instance.Cam.ScreenPointToRay(pos);

        /* "out" gives us result at the end of operation 
           Recommended to pass distance variable over Mathf.Infinity */
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {

            /* Be sure to add != null to interface class check or it won't work.
               Checking against interface is better than checking against a specific
               class, as it can be shared*/
            if (hitInfo.collider.GetComponent<IAttackable>() != null)
            {
                AttackTarget(hitInfo.collider.GetComponent<IAttackable>());
            }

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameManager.Instance.PlayerChangeLives(1);
        }
    }

}
