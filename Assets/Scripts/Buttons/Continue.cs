using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continue : MonoBehaviour
{
    public GameObject panel;
    public void resumeGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

}
