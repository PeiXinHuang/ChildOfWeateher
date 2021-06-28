using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScripts : MonoBehaviour
{

    public Slider progressB;
    public bool isLoading = false;
    // Use this for initialization
    void Start()
    {
        if (progressB)
        {
            progressB.value = 0;
        }
    }
   

    void SetLoadingPercentage(float value)
    {
        progressB.value = value / 100;
    }

    IEnumerator LoadScene()
    {
        progressB.gameObject.SetActive(true);
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync("MainScene");
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

    public void BeginLoadScene()
    {
        if (!isLoading)
        {
            StartCoroutine(LoadScene());
            isLoading = true;
        }
        
    }
}
