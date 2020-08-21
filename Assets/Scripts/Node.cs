using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    private int turretUpgradeLevel = 0;
    

    private Color startColor;
    private float startGlossiness;
    private Renderer rend;
    private BuildManager buildManager;


    private void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        startGlossiness = rend.material.GetFloat("_Glossiness");

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnMouseUpAsButton() {
        if (EventSystemTouch.IsPointerOverGameObject()) {
            return; 
        }

        buildManager.SelectNode(this);
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (turret != null) {
            Debug.Log("Can't build here, turret already exists");
            return;
        }

        if (PlayerStats.money < blueprint.cost) {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        GameObject builtTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = builtTurret;

        turretBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        int upgradeCost = GetUpgradeCost();
        if (PlayerStats.money < upgradeCost) {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        PlayerStats.money -= upgradeCost;

        Destroy(turret);

        GameObject upgradedTurret = (GameObject)Instantiate(turretBlueprint.upgradeConfig.GetPrefab(turretUpgradeLevel + 1), GetBuildPosition(), Quaternion.identity);
        turret = upgradedTurret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        turretUpgradeLevel += 1;
        Debug.Log("Turret upgraded to level " + turretUpgradeLevel);
    }

    public int GetUpgradeLevel()
    {
        return turretUpgradeLevel;
    }

    public int GetUpgradeCost()
    {
        return turretBlueprint.upgradeConfig.GetCosts(turretUpgradeLevel + 1);
    }

    public bool TurretIsUpgradable()
    {
        return turretUpgradeLevel < 3;
    }

    public void SetMaterialHover()
    {
        rend.material.color = hoverColor;
        rend.material.SetFloat("_Glossiness", 0f);
    }

    public void ResetMaterial()
    {
        rend.material.color = startColor;
        rend.material.SetFloat("Glossiness", startGlossiness);
    }
}
