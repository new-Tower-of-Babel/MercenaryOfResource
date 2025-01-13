using UnityEngine;

public class UpgradeSceneManager : MonoBehaviour
{
    public static UpgradeSceneManager instance;
    public GameObject gradeUpgradeUI;
    public GameObject characterUpgradeUI;
    public GameObject weaponUpgradeUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gradeUpgradeUI = ObjectManager.CreateObject<GameObject>("UpgradeScene/GradeUpgradeUI");
        characterUpgradeUI = ObjectManager.CreateObject<GameObject>("UpgradeScene/CharacterUpgradeUI");
        weaponUpgradeUI = ObjectManager.CreateObject<GameObject>("UpgradeScene/WeaponUpgradeUI");
        
    
        gradeUpgradeUI.SetActive(true);
        characterUpgradeUI.SetActive(false);
        weaponUpgradeUI.SetActive(false);
    }
    void Update()
    {
        
    }
   
}
