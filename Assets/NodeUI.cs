using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    
    public Text upgradeCostText;
    
    public Button upgradeButton;

    private Node target;

    private int upgradeCost;

    private void Start() {
        InvokeRepeating("UpdateButtonStatus", 0f, 0.5f);
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (target.TurretIsUpgradable()) {
            upgradeCost = target.GetUpgradeCost();
            upgradeCostText.text = "$" + upgradeCost;
            upgradeButton.interactable = PlayerStats.money >= upgradeCost;
        } else {
            upgradeCostText.text = "MAX";
            upgradeButton.interactable = false;
        }

        ui.SetActive(true);
    }

    private void UpdateButtonStatus() {
        if (!ui.activeSelf) {
            return;
        }

        upgradeButton.interactable = PlayerStats.money >= upgradeCost;
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
