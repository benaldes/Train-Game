using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject LoadLevelButton,ContinueButton, spaceText;
    public Slider LevelSlider;

    private AsyncOperation loadOperation;

    private string saveFilePath;
    private string saveFileData;

    private int levelToLoad;

    private void Start()
    {
        saveFilePath = Application.dataPath + "/SaveFile.txt";
        ReadSaveFile();
    }

    private void ReadSaveFile()
    {
        saveFileData = File.ReadAllText(saveFilePath);
        Debug.Log(saveFileData);
        switch (saveFileData)
        {
            case "0":
                ContinueButton.gameObject.SetActive(false);
                levelToLoad = 0;
                break;
            case "1":
                ContinueButton.gameObject.SetActive(true);
                levelToLoad = 1;
                break;
            case "2":
                ContinueButton.gameObject.SetActive(true);
                levelToLoad = 2;
                break;
        }
    }

    public void LoadNextLevelBtn()
    {
        Destroy(LoadLevelButton);

        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        LevelSlider.gameObject.SetActive(true);
        spaceText.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        spaceText.GetComponent<TextMeshProUGUI>().text = "Press Space";
        loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
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
