using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static float xMin = -8.2f;
    public static float xMax = 5.2f;
    public static float yMin = -5f;

    public GameObject turret;
    public GameObject panel;

    public Text healthText;
    public Text scoreText;
    public Text[] resourcesText = new Text[4];

    private int score;

    private float gameTime;
    private bool firstMenuClicked;

    private TurretController turretController;

    private static GameObject instance;

    public static GameObject Instance { get { return instance; } }

    public int Score
    {
        get => score; set
        {
            score = value;
            UpdateScore();
        }
    }

    internal void UpdateResources()
    {
        Resource resources = turretController.Resources;
        resourcesText[0].text = resources.stone.ToString();
        resourcesText[1].text = resources.metal.ToString();
        resourcesText[2].text = resources.chondrule.ToString();
        resourcesText[3].text = resources.crystal.ToString();
    }

    private void Awake()
    {
        DestroyOld();
        turretController = turret.GetComponent<TurretController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        DestroyOld();
        Score = 0;
        gameTime = 0;
        firstMenuClicked = false;
        turretController = turret.GetComponent<TurretController>();
        UpdateHealth();
        UpdateScore();
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        if (gameTime > 1 && !firstMenuClicked)
        {
            PauseGame();
        }
    }

    public void UpdateHealth()
    {
        healthText.text = turretController.Health.ToString();
        if (turretController.Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
    public void UpdateScore()
    {
        scoreText.text = Score.ToString();
    }

    private void DestroyOld()
    {
        if (instance != null && instance != gameObject)
        {
            DestroyImmediate(instance);
        }
        instance = gameObject;

        DontDestroyOnLoad(gameObject);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        firstMenuClicked = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
