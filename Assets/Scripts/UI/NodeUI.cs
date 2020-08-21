using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    [Header("Shop")]
    public GameObject shopUi;
    public Shop shop;
    public Button standardTurretButton;
    public Button missileLauncherButton;
    public Button laserBeamerButton;

    [Header("Upgrade")]
    public GameObject upgradeUi;
    public Image turretImage;
    public Button upgradeButton;
    public Text upgradeCostText;
    public Text levelText;

    private Node target;

    private void Update() {
        UpdateUI();
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
        shopUi.SetActive(false);
        upgradeUi.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();

        UpdateUpgradeUI();
    }

    private void UpdateUI() {
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

        if (upgradeUi.activeSelf == false) {
            upgradeUi.SetActive(true);
            turretImage.sprite = target.turretBlueprint.image.sprite;
        }
        
        levelText.text = "LVL " + target.GetUpgradeLevel();

        if (target.TurretIsUpgradable()) {
            upgradeCostText.text = "$" + target.GetUpgradeCost();
        } else {
            upgradeCostText.text = "MAX";
            upgradeButton.interactable = false;

            return;
        }

        upgradeButton.interactable = PlayerStats.CanAfford(target.GetUpgradeCost());
    }
}
