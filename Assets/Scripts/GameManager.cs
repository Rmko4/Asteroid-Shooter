using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float xMin = -8.2f;
    public static float xMax = 5.2f;
    public static float yMin = -5f;

    public GameObject turret;

    public Text healthText;
    public Text scoreText;

    private int score;

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

    private void Awake()
    {
        DestroyOld();
    }
    // Start is called before the first frame update
    void Start()
    {
        DestroyOld();
        Score = 0;
        turretController = turret.GetComponent<TurretController>();
        UpdateHealth();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {

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
}
