using UnityEngine;

[CreateAssetMenu(menuName = "SOData/GunslingerStats")]
public class GunslingerStatsSO : ScriptableObject
{
    public string CharacterName;
    public int maxHealth;
    public float moveSpeed;
    public int axeDamage;
    public int pickaxeDamage;
    public float rockHarvestingSpeed;
    public float woodHarvestingSpeed;
    public int resourceCarryLimit;
    public int needCoin;
}
