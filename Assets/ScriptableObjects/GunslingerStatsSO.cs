using UnityEngine;

[CreateAssetMenu(menuName = "SOData/GunslingerStats")]
public class GunslingerStatsSO : ScriptableObject
{
    public string CharacterName;
    public int maxHealth;
    public float moveSpeed;
    public float axeDamage;
    public float pickaxeDamage;
    public float rockHarvestingSpeed;
    public float woodHarvestingSpeed;
    public int resourceCarryLimit;
}
