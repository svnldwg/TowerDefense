using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;
    
    public int upgradeCost1;

    public GameObject upgradedPrefab1;
    public int upgradeCost2;

    public GameObject upgradedPrefab2;
    public int upgradeCost3;

    public GameObject upgradedPrefab3;

    public int GetUpgradeCost(int level)
    {
        if (level == 1) {
            return upgradeCost1;
        } 
        if (level == 2) {
            return upgradeCost2;
        } 
        if (level == 3) {
            return upgradeCost3;
        }

        throw new System.Exception("invalid upgrade level");
    }

    public GameObject GetUpgradePrefab(int level)
    {
        if (level == 1) {
            return upgradedPrefab1;
        } 
        if (level == 2) {
            return upgradedPrefab2;
        } 
        if (level == 3) {
            return upgradedPrefab3;
        }

        throw new System.Exception("invalid upgrade level");
    }
}
