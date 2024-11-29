using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    private Dictionary<string, UIBase> _uiDic = new Dictionary<string, UIBase>();

    public T GetUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;

        if (IsExist<T>())
            return _uiDic[uiName] as T;
        else
            return CreateUI<T>();
    }

    private T CreateUI<T>() where T : UIBase
    {
        var uiName = typeof(T).Name;

        T uiRes = Resources.Load<T>($"PathInfo.UIPath{uiName}");
        var uiObj = Instantiate(uiRes);

        if (IsExist<T>())
            _uiDic[uiName] = uiObj;
        else
            _uiDic.Add(uiName, uiObj);

        return uiObj;
    }

    public bool IsExist<T>()
    {
        var uiName = typeof(T).Name;
        return _uiDic.ContainsKey(uiName) && _uiDic[uiName] != null;
    }


    public T OpenUI<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        ui.Open();
        return ui;
    }

    public T CloseUI<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        ui.Close();

        return ui;
    }


    public void Clear()
    {
        _uiDic.Clear();
    }
}
