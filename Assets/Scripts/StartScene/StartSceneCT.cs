using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneCT : MonoBehaviour
{
    public GameObject helpPanel;
   public void LoadMainScene()
    {
        //SceneManager.LoadScene("MainScene");
        this.GetComponent<LoadingScripts>().BeginLoadScene();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ShowHelpPanel()
    {
        helpPanel.SetActive(true);
    }
    public void HideHelpPanel()
    {
        helpPanel.SetActive(false);
    }
}
