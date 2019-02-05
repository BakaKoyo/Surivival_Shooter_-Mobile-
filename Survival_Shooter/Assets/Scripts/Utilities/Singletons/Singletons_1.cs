using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
We'll take in a generic type <T> that inherits from "Monobehaviour", but
also has to be a "Monobehavior" 'where T: MonoBehaviour' 

    [ How to use ]
    Example: 

    // Notice we declare our class where T should be (Take type of)
    public class GameManager : Singleton<GameManager>
   
*/
public abstract class Singletons_1<T> : MonoBehaviour where T: MonoBehaviour 
{

    protected static T _Instance;

    /* Gets set once called from outside script */
    public static T Instance {
        get { /* Sets _Instance if _Instance is not already set */
            if (_Instance == null) {
                /* Find object of type generic TYPE inherited from monobehavior */
                _Instance = FindObjectOfType<T>();
            }
            return _Instance;
        }
    }


    /* Try to avoid overriding unity methods (Blue Functions) as unity finds them by reflections */
    private void Awake()
    {

        /* Instead, overside a function inside of awake */
        OnAwake();

    }

    protected virtual void OnAwake()
    {

        /* Destroy singleton gameobject copy if there is more than 1 */
        if (Instance != this) Destroy(gameObject);

    }

}
