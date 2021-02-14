using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public int type;

    private Button button;
    private Image image;
    private TurretController turretController;

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        GameObject turret = GameObject.Find("Turret");
        turretController = turret.GetComponent<TurretController>();
    }

    private void Update()
    {
        if (turretController.CanUpgrade[type])
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    public void RoFUpgrade()
    {
        turretController.UpgradeRoF();
    }

    public void MultiShotUpgrade()
    {
        turretController.UpgradeMultiShot();
    }
    public void ConeUpgrade()
    {
        turretController.UpgradeCone();
    }

    public void Enable()
    {
        button.interactable = true;
        image.color = Color.white;
    }

    public void Disable()
    {
        button.interactable = false;
        image.color = new Color(0.3f, 0.3f, 0.3f);
    }
}
