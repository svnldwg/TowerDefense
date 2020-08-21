using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public GameObject shopUi;
    public GameObject upgradeUi;
    public Shop shop;
    public Button standardTurretButton;
    public Button missileLauncherButton;
    public Button laserBeamerButton;
    public Button upgradeButton;
    public Text upgradeCostText;
    public Text levelText;

    private Node target;

    private void Start() {
        InvokeRepeating("UpdateUI", 0f, 0.25f);
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        ui.SetActive(true);
        UpdateUI();
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();

        UpdateUpgradeUI();
    }

    public void UpdateUpgradeTexts()
    {
        levelText.text = "LVL " + target.GetUpgradeLevel();

        if (target.TurretIsUpgradable()) {
            upgradeCostText.text = "$" + target.GetUpgradeCost();
        } else {
            upgradeCostText.text = "MAX";
        }
    }

    public void UpdateUI() {
        if (!ui.activeSelf) {
            return;
        }

        UpdateShopUI();
        UpdateUpgradeUI();
    }

    private void UpdateShopUI()
    {
        if (target.turret != null) {
            shopUi.SetActive(false);
            return;
        }
        
        shopUi.SetActive(true);

        standardTurretButton.interactable = PlayerStats.money >= shop.standardTurret.cost;
        missileLauncherButton.interactable = PlayerStats.money >=  shop.missileLauncher.cost;
        laserBeamerButton.interactable = PlayerStats.money >= shop.laserBeamer.cost;
    }

    private void UpdateUpgradeUI()
    {
        if (target.turret == null) {
            upgradeUi.SetActive(false);
            return;
        }

        upgradeUi.SetActive(true);
        
        if (!target.TurretIsUpgradable()) {
            upgradeButton.interactable = false;

            return;
        }

        upgradeButton.interactable = PlayerStats.CanAfford(target.GetUpgradeCost());

        UpdateUpgradeTexts();
    }
}
