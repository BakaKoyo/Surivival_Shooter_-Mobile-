using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //This variable will be used to determine who its intened to hit
    private string targetName;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void BulletSetup(string target)
    {
        targetName = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetName)
        {
            GameManager.Instance.PlayerChangeScore(1);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
