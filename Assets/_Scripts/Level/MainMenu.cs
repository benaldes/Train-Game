using System.Collections;
using System.IO;
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
        saveFileData = File.ReadAllText(saveFilePath).Trim();
        
        Debug.Log(saveFileData);
        
        switch (saveFileData)
        {
            case "1":
                ContinueButton.gameObject.SetActive(false);
                levelToLoad = 1;
                break;
            case "2":
                ContinueButton.gameObject.SetActive(true);
                levelToLoad = 2;
                break;
            
        }
    }

    public static void WriteToFile(string context)
    {
        string FilePath = Application.dataPath + "/SaveFile.txt";
        
        StreamWriter writer = new StreamWriter(FilePath, false);
        
        writer.WriteLine(context);
        writer.Close();
    }

    public void NewGame()
    {
        WriteToFile("1");
        ReadSaveFile();
        LoadNextLevelBtn();
    }
    
    

    public void LoadNextLevelBtn()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        
        LoadLevelButton.gameObject.SetActive(false);
        LevelSlider.gameObject.SetActive(true);
        spaceText.gameObject.SetActive(true);
        ContinueButton.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(2);
        Debug.Log("before load1");
        spaceText.GetComponent<TextMeshProUGUI>().text = "Press Space";
        Debug.Log("before load2");
        loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        if(loadOperation == null) Debug.Log("load null");
        loadOperation.allowSceneActivation = false;
        
        while (!loadOperation.isDone)
        {
            Debug.Log("Loading");
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            LevelSlider.value = progressValue;
            yield return null;
        }
    }

    private void Update()
    {
        if (loadOperation != null)
        {
            Debug.Log("load not null");
            if (loadOperation.progress >= 0.9f)
            {
                Debug.Log("load Doon");
                spaceText.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Space press");
                    loadOperation.allowSceneActivation = true;
                }
            }
        }
    }
}
