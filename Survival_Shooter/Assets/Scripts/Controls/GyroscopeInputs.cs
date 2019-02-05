using UnityEngine;

public class GyroscopeInputs : MonoBehaviour
{

    /* 
    Same as a regular variable, expect standard naming convention
    starts with Capital and is alternative to making a seperate 
    get function.
    

        Instead of doing
        -----------------------------
        Gyroscope Gyro;
        public void GetGyroscope()
        {
           return Gyro
        }
        -----------------------------
    
    */
    public Gyroscope Gyro { get; private set; }

    Quaternion rot;

    private void Start()
    {
        EnableGyro();
    }


    void EnableGyro()
    {
        /* 
        Checks the device if it supports gyro 
        (Note: Not all phones gyro)
        */
        if (SystemInfo.supportsGyroscope)
        {
            Gyro = Input.gyro;
            Gyro.enabled = true;

            /* Quarternion values work from 0 to 1 */
            rot = new Quaternion(0f, 0f, 1f, 0f);

        }
        else
        {
            Debug.Log(" WARNING: YOUR DEVICE DOES NOT SUPPURT GYROSCOPE! ");
            Destroy(this);
        }
    }

    /*
    Return function: has to return type declared
    Void is a none return function 
    */
    public Quaternion GetRotation()
    {
        return Gyro.attitude * rot;
    }

    

}
