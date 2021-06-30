using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIController : MonoBehaviour
{

    

    #region 对话框相关

    //对话对话框
    public GameObject message;
    public Text messageName;
    public Text messageText;

    public void ShowMessage(string name,string text)
    {
        qmessage.SetActive(false);
        message.SetActive(true);
        messageName.text = name;
        messageText.text = text;
        StopCoroutine(HideMessage());
        StartCoroutine(HideMessage());
    }
    public void HideMessageImmediate()
    {
        StopCoroutine(HideMessage());
        message.SetActive(false);
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(2.0f);
        message.SetActive(false);
    }

    //问题对话框
    public GameObject qmessage;
    public Text qmessageName;
    public Text qmessageText;
    public List<Text> ans = new List<Text>();
    public void ShowQMessage(string name, string text, string ans1,string ans2, string ans3, string ans4)
    {
        message.SetActive(false);
        qmessage.SetActive(true);
        qmessageName.text = name;
        qmessageText.text = text;
        StopCoroutine(HideQMessage());
        StartCoroutine(HideQMessage());
        ans[0].text = "A " + ans1;
        ans[1].text = "B " + ans2;
        ans[2].text = "C " + ans3;
        ans[3].text = "D " + ans4;
    }

    private IEnumerator HideQMessage()
    {
        yield return new WaitForSeconds(5.0f);
        qmessage.SetActive(false);
    }

    #endregion

    #region CTUI相关
    public GameObject CTUI;
    public List<GameObject> CTChildUI = new List<GameObject>();
    public void ShowCTUI()
    {
        CTUI.LeanMoveLocalX(0, 0.5f);
        CTUI.SetActive(true);
    }
    public void HideCTUI()
    {
        CTUI.LeanMoveLocalX(-800.0f, 0.5f);
        StartCoroutine(HideCTUIforSecond());
    }
    public IEnumerator HideCTUIforSecond()
    {
        yield return new WaitForSeconds(0.5f);
        CTUI.SetActive(false);
    }


    //显示子面板
    public void ShowCTChildUI(int index)
    {
        foreach (GameObject gameObject in CTChildUI)
        {
            gameObject.SetActive(false);
        }
        CTChildUI[index].SetActive(true);

        if (index == 0)
        {
            SetTaskPanel();
        }
        else if(index == 1)
        {
            SetSkyPanel();
        }
        else if(index == 2)
        {
            SetWeatherPanel();
        }
    }

    //任务菜单相关
    public List<GameObject> tasks = new List<GameObject>();
    public Text taskDis;
    public void SetTaskPanel()
    {
        bool hasGetTask = GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasGetTask();
        if (hasGetTask)
        {
            foreach (GameObject task in tasks)
            {
                task.SetActive(true);
            }
            tasks[0].SetActive(false);
        }
        else
        {
            foreach (GameObject task in tasks)
            {
                task.SetActive(false);
            }
            tasks[0].SetActive(true);
        }
    }

    //设置任务描述
    public void SetTaskDis(int textid)
    {
        if(textid == 0)
        {
            taskDis.text = "到春天场景里，点击拼图，完成拼图游戏，就可以获得春天之石，如果没有完成拼图就退出，扣除一滴血";
        }

        else if(textid == 1)
        {
            taskDis.text = "到夏天场景里，踩着荷叶到湖中央就可以收集到夏天之石，如果不小心掉到湖中，将会扣掉一滴血";
        }

        else if(textid == 2){
            taskDis.text = "到秋天场景里，点击显微镜，获取信息，然后到巫师那里回答问题，回答正确获得秋天宝石，靠近精灵会有问题提示，每次回答错误扣掉一滴血";
        }

        else if(textid == 3)
        {
            taskDis.text = "到冬天场景里，看电视学习制作火鸡的方法，收集食材后到圣诞老人那里制作火鸡,制作完成获得冬天宝石，如果食材没有收集完毕就开始制作火鸡，扣掉一滴血";
        }
      
    }

    //完成任务时修改子任务面板
    public void SetTaskChild(int index)
    {
        tasks[index + 1].transform.GetChild(1).gameObject.SetActive(false);
        tasks[index + 1].transform.GetChild(2).gameObject.SetActive(true);
    }

    //天气面板相关
    public GameObject weatherPanel1;
    public GameObject weatherPanel2;
    public void SetWeatherPanel()
    {
        bool hasGetTask = GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasFinishAllTask();
        if (hasGetTask)
        {
            weatherPanel1.SetActive(false);
            weatherPanel2.SetActive(true);
        }
        else
        {
            weatherPanel2.SetActive(false);
            weatherPanel1.SetActive(true);
        }
    }


    //天空面板相关
    public GameObject skyPanel1;
    public GameObject skyPanel2;
    public void SetSkyPanel()
    {
        bool hasGetTask = GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasFinishAllTask();
        if (hasGetTask)
        {
            skyPanel1.SetActive(false);
            skyPanel2.SetActive(true);
        }
        else
        {
            skyPanel2.SetActive(false);
            skyPanel1.SetActive(true);
        }
    }



    #endregion

    #region 浮动面板相关
    public GameObject floatPanel;
    public void ShowFloatPanel()
    {
        floatPanel.SetActive(true);
    }
    public void HideFloatPanel()
    {
        floatPanel.SetActive(false);
    }
    public void SetFloatPanelPos(Vector3 pos)
    {
        floatPanel.transform.position = pos;
    }

    #endregion

    #region Puzzle面板相关
    public GameObject puzzlePanel;
    public void ShowPuzzlePanel()
    {
        puzzlePanel.SetActive(true);
    }
    public void HidePuzzlePanel()
    {
        puzzlePanel.SetActive(false);
    }
    #endregion

    #region 显微镜面板相关
    public GameObject microscopePanel;
    public void ShowMicroscopePanel()
    {
        microscopePanel.SetActive(true);
    }
    public void HideMicroscopePanel()
    {
        microscopePanel.SetActive(false);
    }
    #endregion

    #region 爱心面板相关
    public List<GameObject> hearts = new List<GameObject>();
    public void SetHeartNum(int num)
    {
        hearts[num].SetActive(false);
    }
    #endregion

    #region MessageBox相关
    public GameObject loseMessageBox;
    public void ShowLoseMessageBox()
    {
        loseMessageBox.SetActive(true);
    }
    public GameObject winMessageBox;
    public void ShowWinMessageBox()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCT>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled = false;
        Camera.main.GetComponent<CameraCT2>().enabled = false;
        winMessageBox.SetActive(true);
    }
    public void HideWinMessageBox()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCT>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled = true;
        Camera.main.GetComponent<CameraCT2>().enabled = true;
        winMessageBox.SetActive(false);
    }
    #endregion


    #region 切换面板相关

    [SerializeField]private GameObject SeasonPanel; 
    public void ShowSeasonPanel()
    {
        SeasonPanel.SetActive(true);
    }
    public void HideSeasonPanel()
    {
        SeasonPanel.SetActive(false);
    }
    #endregion

    #region 视频面板相关
    [SerializeField] private GameObject videoPanel;

    public IEnumerator ShowVideo()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(EndVideo());
    }
    public IEnumerator EndVideo()
    {
        yield return new WaitForSeconds(4);
        videoPanel.SetActive(false);
    }

    #endregion

    #region 黑屏过渡相关
    
    [SerializeField] private Image fadeImg = null;
    public void ShowFadeImg()
    {
        fadeImg.gameObject.SetActive(true);
        fadeImg.DOFade(1, 0.5f);
        StartCoroutine(SetImageNotActive(0.5f));
    }
    public IEnumerator SetImageNotActive(float second)
    {
        yield return new WaitForSeconds(second);
        fadeImg.DOFade(0, 0.5f);
        StartCoroutine(SetImageNotActiveForSecond(0.5f));
    }
    public IEnumerator SetImageNotActiveForSecond(float second)
    {
        yield return new WaitForSeconds(second);
        fadeImg.gameObject.SetActive(false);
    }
    #endregion


}
