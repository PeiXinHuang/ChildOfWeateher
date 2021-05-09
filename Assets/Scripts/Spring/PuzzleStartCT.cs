using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStartCT : MonoBehaviour
{
    public PlayerCT playerCT;
    public float distance = 5.0f;

    public NPCCT nPCCT;
    public GameObject jewel;


    //鼠标按下，进入拼图模式,主角无法行走，显示puzzleUI,puzzle可以被操纵,显示puzzle摄像机
    private void OnMouseDown()
    {
        //还没有接收任务，不可以进行拼图
        if (Vector3.Distance(this.transform.position, playerCT.transform.position)< distance){

            if (!GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasGetTask())
            {
                nPCCT.SayMessage("你需要先接收完任务才可以开始拼图");
                return;
            }

            playerCT.enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<PuzzleCT>().enabled = true;
            GameObject.Find("SceneController").GetComponent<UIController>().ShowPuzzlePanel();
            GameObject.Find("SceneController").GetComponent<SceneCT>().SetCamPuzzle();
        }
    }
    
    //结束拼图，与结束拼图模式相反
    public void EndPuzzle()
    {
        playerCT.enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<PuzzleCT>().enabled = false;
        GameObject.Find("SceneController").GetComponent<UIController>().HidePuzzlePanel();
        GameObject.Find("SceneController").GetComponent<SceneCT>().SetCamMain();

        if (GetComponent<PuzzleCT>().ChargePuzzle())
        {
            if (GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasFinishTask(0))
            {
                nPCCT.SayMessage("虽然你已经完成了拼图，但是春天之石已经给你了，去试试其它任务吧");
            }
            else
            {
                jewel.SetActive(true);
                nPCCT.SayMessage("太棒了,你已经完成拼图,春天之石是属于你的了");
            }
            
           
        }
        else
        {
            GameObject.Find("SceneController").GetComponent<SceneCT>().LostLife();
            nPCCT.SayMessage("你还未完成拼图，可以多试几次，加油!!!");
           
        }
    }
}
