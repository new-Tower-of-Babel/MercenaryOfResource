using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster", menuName = "New Monster")]
public class MonsterData : ScriptableObject
{
    [Header("Info")]
    public string monsterName;

    [Header("Combat")]
    public float moveSpeed;
    public int health;
    public float attackDistance;
    public float attackRate;


}