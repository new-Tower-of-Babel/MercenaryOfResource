using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonsterSpawner))]
public class MonsterGenerateButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var spawner = (MonsterSpawner)target;
        if (GUILayout.Button ("Generate Monster"))
        {
            spawner.SpawnMonster();
        }
    }
}
