using UnityEngine;

public static class ResourceManager
{
    private static string soDataPath = "SOData";

    public static T LoadSOData<T> (string name) where T : ScriptableObject
    {
        var data = Resources.Load<T> ($"{soDataPath}/{name}");
        
        if (data == null)
            Debug.Log ($"Error: {name} of type {typeof(T).Name} not found in {soDataPath}");

        return data;
    }
}
