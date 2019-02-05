using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputs : MonoBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawnPoint;

    public float bulletSpeed;

    bool isFingerDown = false;
    float fingerDownPosX;
    int fingerDownIndex = -1;

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }

        /* Check for presses, however setup Listener 
           events (more advanced) for pressed */
        OnTouch();

    }

    public void PlayerRotation(Vector2 rot)
    {
        transform.Rotate(rot, Space.World);
    }

    void OnTouch()
    {
        /* Check for the amount of finger touches on screen */
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch touch = Input.GetTouch(i);

            /* Perform action depending on action of touch (Press, Move, etc) */
            switch (touch.phase)
            {

                case TouchPhase.Began:
                    OnTouchBegan(touch, i);
                    break;

                case TouchPhase.Ended:
                    OnTouchEnded(i);
                    break;

                case TouchPhase.Moved:
                    OnTouchMoved(fingerDownPosX - touch.deltaPosition.x, i);
                    break;

            }

        }

    }


    void OnTouchBegan(Touch touch, int Index)
    {

        /* Any finger can register the attack -- leave outside of is finger down check */
        GameManager.Instance.Player.HitCheck(touch.position);

        ShootProjectile();

        if (isFingerDown == false)
        {
            isFingerDown = true;
            fingerDownIndex = Index;

            /* Keeps track of our finger position on the screen to know 
              if we are moving our finger either left or right */
            fingerDownPosX = touch.deltaPosition.x;
        }

    }

    void OnTouchEnded(int Index)
    {
        if(Index == fingerDownIndex)
        {
            /* Set it to a number that is not in the touch index array (0+) */
            fingerDownIndex = -1;

            /* Our finger in control of moving is no longer down.
               another finger can now be incontrol on move */
            isFingerDown = false;
        }
    }


    void OnTouchMoved(float posX, int Index)
    {
       // /* Only want 1 finger to move, so check against finger */
       // if (GameManager.Instance.Player != null && Index == fingerDownIndex)
       // {
       //     posX /= 3;
       //     GameManager.Instance.Player.PlayerRotation(Vector2.up * -posX);
       // }

    }

    //This function will create a bullet from the barrel of the gun
    public void ShootProjectile()
    {
        GameObject _bullet;
        _bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        _bullet.GetComponent<Bullet>().BulletSetup("Enemy");

        //This line applies velocity in the direction where the spawn point is facing
        _bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward;
    }

}
