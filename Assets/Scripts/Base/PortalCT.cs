using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCT : MonoBehaviour
{
    public UIController uIController;
    public float distance = 5.0f;

    public PlayerCT player;

    private void OnTriggerEnter(Collider other)
    {
        uIController.ShowMessage("提示","回车显示季节切换面板");
    }

    private void OnTriggerExit(Collider other)
    {
        uIController.HideMessageImmediate();
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return)) && 
           Vector3.Distance(player.transform.position,this.transform.position)<distance)
        {
            if (GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasGetTask())
            {
                uIController.ShowSeasonPanel();
            }
            else
            {
                uIController.ShowMessage("提示", "你需要先到ZERO那里接受任务");
            }
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            uIController.HideSeasonPanel();
        }
    }

}
