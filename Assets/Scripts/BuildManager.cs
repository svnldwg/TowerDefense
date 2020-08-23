using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public Node selectedNode;
    public NodeUI nodeUI;
    public GameObject buildEffect;
    public GameObject sellEffect;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("More than one BuildManager in scene");
            return;
        }
        instance = this;
    }

    public void SelectNode(Node node)
    {
        if (node == selectedNode) {
            DeselectNode();
            return;
        }

        if (selectedNode != null) {
            DeselectNode();
        }
        
        selectedNode = node;
        selectedNode.SetMaterialHover();

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode.ResetMaterial();
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        selectedNode.BuildTurret(turret);
    }
}
