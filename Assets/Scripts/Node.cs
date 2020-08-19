using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;
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

    private void OnMouseEnter() {
        if (EventSystemTouch.IsPointerOverGameObject()) {
            return; 
        }
        if (!buildManager.CanBuild) {
            return;
        }
        setMaterialHover();
    }

    private void OnMouseExit() {
        resetMaterial();
    }

    private void OnMouseUpAsButton() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return; 
        } 

        if (turret != null) {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) {
            return;
        }

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {

        if (PlayerStats.money < blueprint.cost) {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        GameObject builtTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = builtTurret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    private void setMaterialHover()
    {
        if (buildManager.HasMoney) {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = notEnoughMoneyColor;
        }
        rend.material.SetFloat("_Glossiness", 0f);
    }

    private void resetMaterial()
    {
        rend.material.color = startColor;
        rend.material.SetFloat("Glossiness", startGlossiness);
    }
}
