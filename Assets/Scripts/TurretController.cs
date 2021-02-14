using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public GameObject defaultShot;
    public GameObject gameManagerObject;

    private GameManager gameManager;

    public float movementSpeed = 10.0f;
    private float fireDelta = 0.2f;
    private int shots = 1;
    private float cone = 50f;
    private float bulletSpeed = 10f;

    private float health = 100f;
    private Resource resource;

    private int[] upgrades = new int[] { 0, 0, 0 };
    private bool[] canUpgrade = new bool[] { false, false, false };
    private readonly int[] maxUpgrades = new int[] { 5, 2, 20 };
    private Resource[] upgradeCosts = new Resource[] {
        new Resource(15, 0, 6, 0),
        new Resource(30, 15, 10, 4),
        new Resource(4, 2, 3, 1)
    };

    private readonly float xOffset = -1.5f;
    private readonly float yOffset = -4.85f;
    private readonly float earthRadius = 100f;

    Transform[] childBarrel = new Transform[5];

    float timeSinceFire;

    private void NotifyHealth()
    {
        gameManager.UpdateHealth();
    }

    private void NotifyResourceUpdate()
    {
        gameManager.UpdateResources();
    }

    private void Start()
    {
        timeSinceFire = 0f;
        Resources = new Resource(0, 0, 0, 0);

        gameManager = gameManagerObject.GetComponent<GameManager>();
        NotifyResourceUpdate();
        childBarrel[0] = transform.GetChild(0);
        for (int i = 0; i < 4; i++)
        {
            string objectName = "Barrel/Barrel" + (i + 1).ToString();
            childBarrel[i + 1] = transform.Find(objectName);
        }
        UpdateCone();
    }


    void Update()
    {
        Move();
        Fire();
        UpdateUpgrades();
    }

    private void Move()
    {
        float translation = Input.GetAxis("Horizontal") * movementSpeed;
        translation *= Time.deltaTime;

        Vector3 position = transform.position;
        position.x += translation;

        if (position.x < GameManager.xMin)
        {
            position.x = GameManager.xMin;
            transform.position = position;
        }
        else if (position.x > GameManager.xMax)
        {
            position.x = GameManager.xMax;
            transform.position = position;
        }
        else
        {
            float newX = position.x - xOffset;
            position.y = Mathf.Sqrt(earthRadius * earthRadius - newX * newX) - earthRadius + yOffset;
            transform.position = position;
        }
    }

    private void Fire()
    {
        if (Input.GetAxis("Fire1") == 1f && timeSinceFire > fireDelta)
        {
            timeSinceFire = 0;
            for (int i = 0; i < shots; i++)
            {
                Transform barrelTransform = childBarrel[i];
                Vector3 positionClone = barrelTransform.position;
                Quaternion rotationClone = barrelTransform.rotation;
                float theta = rotationClone.eulerAngles.z + 90f;
                GameObject cloneShot = Instantiate(defaultShot, positionClone, rotationClone);
                Rigidbody2D rb2d = cloneShot.GetComponent<Rigidbody2D>();

                float x = Mathf.Cos(theta * Mathf.Deg2Rad);
                float y = Mathf.Sin(theta * Mathf.Deg2Rad);

                rb2d.velocity = new Vector2(x, y) * bulletSpeed;
            }

        }
        timeSinceFire += Time.deltaTime;
    }

    private void UpdateUpgrades()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            if(maxUpgrades[i] > upgrades[i] && resource >= upgradeCosts[i])
            {
                CanUpgrade[i] = true;
                if (i == 2 && shots == 1)
                {
                    CanUpgrade[i] = false;
                }
                
            } else
            {
                CanUpgrade[i] = false;
            }
        }
    }

    public void UpgradeRoF()
    {
        if (resource >= upgradeCosts[0])
        {
            resource -= upgradeCosts[0];
            upgradeCosts[0].ScaleResource(3);
            upgrades[0]++;
            fireDelta *= 0.75f;
            NotifyResourceUpdate();
        }
    }
    internal void UpgradeMultiShot()
    {
        if (resource >= upgradeCosts[1])
        {
            resource -= upgradeCosts[1];
            upgradeCosts[1].ScaleResource(2);
            upgrades[1]++;
            shots += 2;
            for (int i = 0; i < shots; i++)
            {
                childBarrel[i].gameObject.SetActive(true);
            }
            NotifyResourceUpdate();
        }
    }

    internal void UpgradeCone()
    {
        if (resource >= upgradeCosts[2])
        {
            resource -= upgradeCosts[2];
            upgradeCosts[2].ScaleResource(2);
            upgrades[2]++;
            cone *= 0.85f;
            UpdateCone();
            NotifyResourceUpdate();
        }
    }

    private void UpdateCone()
    {
        childBarrel[1].localEulerAngles = new Vector3(0, 0, cone / 2);
        childBarrel[2].localEulerAngles = new Vector3(0, 0, -cone / 2);
        childBarrel[3].localEulerAngles = new Vector3(0, 0, cone);
        childBarrel[4].localEulerAngles = new Vector3(0, 0, -cone);
    }


    public float Health
    {
        get => health;
        set
        {
            health = value;
            NotifyHealth();
        }
    }

    public Resource Resources { get => resource; set => resource = value; }
    public bool[] CanUpgrade { get => canUpgrade; set => canUpgrade = value; }

    public void AddResources(Resource newResource)
    {
        Resources += newResource;
        NotifyResourceUpdate();
    }
}
