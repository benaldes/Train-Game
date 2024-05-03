using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject EndLevelScreen, spaceText;
    public Slider LevelSlider;
    
    private AsyncOperation loadOperation;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }
    public void LoadNextLevel()
    {
        
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        EndLevelScreen.SetActive(true);
        LevelSlider.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        text.text = "press space";
        loadOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
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
