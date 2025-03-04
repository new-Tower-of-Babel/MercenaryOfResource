using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null)
                {
                    var singletoneObject = new GameObject(typeof(T).ToString());
                    instance = singletoneObject.AddComponent<T>();
                    DontDestroyOnLoad(instance);
                }
            }

            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

    public virtual void Start()
    {

    }

    public virtual void Release()
    {
        if(instance == null)
            return;

        if(instance.gameObject)
            Destroy(instance.gameObject);

        instance = null;
    }
}
