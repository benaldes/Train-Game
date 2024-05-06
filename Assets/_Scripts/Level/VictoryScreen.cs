using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject MainMenuButton, ExitButton;

    public void OnLoading()
    {
        MainMenuButton.SetActive(false);
        ExitButton.SetActive(false);
    }
    
}
