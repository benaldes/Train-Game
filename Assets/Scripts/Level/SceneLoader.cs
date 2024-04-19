using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public GameObject LoadLevelButton, spaceText;
    public Slider LevelSlider;

    private AsyncOperation loadOperation;
    
    public void LoadNextLevelBtn()
    {
        
        Destroy(LoadLevelButton);

        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        LevelSlider.gameObject.SetActive(true);
        loadOperation = SceneManager.LoadSceneAsync(1);
        loadOperation.allowSceneActivation = false;
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LevelSlider.value = progressValue;
            yield return null;
        }
    }

    private void Update()
    {
        if (loadOperation != null)
        {

            if (loadOperation.progress == 0.9f)
            {
                spaceText.SetActive(true);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    loadOperation.allowSceneActivation = true;
                }
            }
        }
    }
}
