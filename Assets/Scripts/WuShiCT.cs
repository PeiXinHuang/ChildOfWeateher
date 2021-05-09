using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuShiCT : NPCCT
{

    public string QName;
    public string QMessage;
    public string ans1;
    public string ans2;
    public string ans3;
    public string ans4;

    public GameObject jewel;

    private void OnMouseDown()
    {
       
        //NPC和主角的距离小于distance时候才发挥作用
        if (Vector3.Distance(this.transform.position, playerCT.transform.position) < distance)
        {
            if (!GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasGetTask())
            {
                SayMessage("你需要先接收完任务才可以开始回答我的问题");
                return;
            }
            base.uiController.ShowQMessage(QName, QMessage, ans1, ans2, ans3, ans4);
        }
        
    }

    public int rightIndex = 0; //正确选项 0 - A 1 - B 2 - C 3 - D
    public void ChargeAns(int index)
    {

        if (index == rightIndex)
        {

            if (GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasFinishTask(1))
            {
                SayMessage("虽然你已经完成了拼图，但是宝石已经给你了");
            }
            else
            {
                jewel.SetActive(true);
                SayMessage("不错哦，竟然猜对了，宝石就给你吧");
            }
        }
        else
        {
            GameObject.Find("SceneController").GetComponent<SceneCT>().LostLife();
            SayMessage("你猜错了，作为惩罚，我要拿走你的爱心");
        }
    }    



}
