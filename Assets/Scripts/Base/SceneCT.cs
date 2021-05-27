using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCT : MonoBehaviour
{
    #region 任务相关

    //获取任务
    [SerializeField]
    private bool hasGetTask = false;
    public bool GetHasGetTask()
    {
        return hasGetTask;
    }
    public void SetHasGetTask()
    {
        hasGetTask = true;
    }

    //完成任务
    [SerializeField]
    private bool[] task = { false, false, false, false };
    public bool GetHasFinishTask(int index)
    {
        return task[index];
    }
    public bool GetHasFinishAllTask()
    {
        foreach (bool item in task)
        {
            if (!item)
                return false;
        }
        return true;
    }
    public void SetFinshTask(int index)
    {
        uiController.SetTaskChild(index);
        task[index] = true;
    }

    public void ChargeFinshAllTask()
    {
        if (task[0] && task[1] && task[2] && task[3])
        {
            uiController.ShowWinMessageBox();
        }
    }

    #endregion

    #region 摄像机切换
    public Camera camMain;
    public Camera camPuzzle;
    public Camera camMicroscope;
    public void SetCamMain()
    {
        camPuzzle.enabled = false;
        camMicroscope.enabled = false;
        camMain.enabled = true;  
    }
    public void SetCamPuzzle()
    {
        camPuzzle.enabled = true;
        camMain.enabled = false;
    }
    public void SetCamMicroscope()
    {
        camMain.enabled = false;
        camMicroscope.enabled = true;
    }
    #endregion


    #region 角色性命相关

    private int lifeNum = 3;
    public void LostLife() //减少一条性命，如果减完等于0，游戏结束
    {
        lifeNum--;
        uiController.SetHeartNum(lifeNum);
        if (lifeNum == 0)
        {
            OverGame();      
        }   
    }
    public int GetLifeNum()
    {
        return lifeNum;
    }
    #endregion



    #region 结束游戏相关
    public UIController uiController;
    public void OverGame()
    {
      
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCT>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>().enabled = false;
        camMain.GetComponent<CameraCT>().enabled = false;
        uiController.ShowLoseMessageBox();
    }

    

    #endregion




    //重新开始游戏
    public void ReStartGame()
    {
        SceneManager.LoadScene("OutSideScene");
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }
}
