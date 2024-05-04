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

    private void Awake()
    {
        isOptionMenuOn = false;
        BabyModeText = false;
    }

    private void Update()
    {
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
