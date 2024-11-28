using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : SingletonBase<ObjectManager>
{
    public T CreateObject<T>(string path) where T : Object
    {
        var res = Resources.Load<T>(path);
        return Instantiate(res);
        //ex - player = ObjectManager.Load<GameObject>(path);
    }
}
