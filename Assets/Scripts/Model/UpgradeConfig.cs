using UnityEngine;

[System.Serializable]
public class UpgradeConfig
{
    public UpgradeLevel level1;

    public UpgradeLevel level2;

    public UpgradeLevel level3;

    public int GetCosts(int level)
    {
        return GetLevelObject(level).costs;
    }

    public GameObject GetPrefab(int level)
    {
        return GetLevelObject(level).prefab;
    }

    private UpgradeLevel GetLevelObject(int level)
    {
        if (level == 1) {
            return level1;
        } 
        if (level == 2) {
            return level2;
        } 
        if (level == 3) {
            return level3;
        }

        throw new System.Exception("invalid upgrade level");
    }
}