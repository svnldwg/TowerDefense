using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;
    public GameObject shopUi;
    public Shop shop;
    public Button standardTurretButton;
    public Button missileLauncherButton;
    public Button laserBeamerButton;

    private Node target;

    private void Start() {
        InvokeRepeating("UpdateButtonStatus", 0f, 0.2f);
    }

    public void SetTarget(Node _target)
    {
        target = _target;

        ui.SetActive(true);
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

    private void UpdateButtonStatus() {
        if (!ui.activeSelf) {
            return;
        }

        if (target.turret != null) {
            shopUi.SetActive(false);
            return;
        } else {
            shopUi.SetActive(true);
        }

        standardTurretButton.interactable = PlayerStats.money >= shop.standardTurret.cost;
        missileLauncherButton.interactable = PlayerStats.money >=  shop.missileLauncher.cost;
        laserBeamerButton.interactable = PlayerStats.money >= shop.laserBeamer.cost;
    }
}
