using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
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
        if (EventSystem.current.IsPointerOverGameObject()) {
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
        if (!buildManager.CanBuild) {
            return;
        }

        if (turret != null) {
            Debug.Log("Can't build there! - TODO; Display on screen");
            return;
        }

        buildManager.BuildTurretOn(this);
    }

    private void setMaterialHover()
    {
        rend.material.color = hoverColor;
        rend.material.SetFloat("_Glossiness", 0f);
    }

    private void resetMaterial()
    {
        rend.material.color = startColor;
        rend.material.SetFloat("Glossiness", startGlossiness);
    }
}
