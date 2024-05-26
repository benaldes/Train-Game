using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionMenu;
    private bool isOptionMenuOn;

    [SerializeField] private TextMeshProUGUI BabyButtonText;
    private bool BabyModeText;

    [SerializeField] private Stats playerStats;

    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        isOptionMenuOn = false;
        BabyModeText = false;
        playerStats.OnHealthZero += GameOver;
    }

    private void OnDestroy()
    {
        playerStats.OnHealthZero -= GameOver;
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0.1f;
    }

    private void Update()
    {
        if(Time.timeScale == 0.1f) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionMenu();
        }
        
    }

    public void ToggleOptionMenu()
    {
        if (isOptionMenuOn)
        {
            Time.timeScale = 1;
            optionMenu.SetActive(false); 
                
        }
        else
        {
            Time.timeScale = 0;
            optionMenu.SetActive(true);
        }

        isOptionMenuOn = !isOptionMenuOn;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchTextOnBabyMode()
    {
        BabyModeText = !BabyModeText;
        if (BabyModeText)
        {
            BabyButtonText.text = "Baby Mode : On";
        }
        else
        {
            BabyButtonText.text = "Baby Mode : Off";
        }
    }
}
