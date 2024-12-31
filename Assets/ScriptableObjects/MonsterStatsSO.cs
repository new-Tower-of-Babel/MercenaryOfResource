using UnityEngine;

[CreateAssetMenu(menuName = "SOData/MonsterStats")]
public class MonsterStatsSO : ScriptableObject
{
    public float health;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;
}
