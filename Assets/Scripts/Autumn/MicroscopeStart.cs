using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroscopeStart : MonoBehaviour
{
    public PlayerCT playerCT;
    public float distance = 5.0f;



    //鼠标按下，进入拼图模式,主角无法行走，显示puzzleUI,puzzle可以被操纵,显示puzzle摄像机
    private void OnMouseDown()
    {
        //还没有接收任务，不可以进行拼图
        if (Vector3.Distance(this.transform.position, playerCT.transform.position) < distance)
        {
            playerCT.enabled = false;
            playerCT.gameObject.SetActive(false);
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<MicroscopeController>().enabled = true;
            this.GetComponent<ModelRotation>().enabled = true;
            GameObject.Find("SceneController").GetComponent<UIController>().ShowMicroscopePanel();
            GameObject.Find("SceneController").GetComponent<SceneCT>().SetCamMicroscope();
        }
    }

    //结束拼图，与结束拼图模式相反
    public void EndMicroscrope()
    {
        playerCT.gameObject.SetActive(true);
        playerCT.enabled = true;
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<MicroscopeController>().enabled = false;
        this.GetComponent<ModelRotation>().enabled = false;
        GameObject.Find("SceneController").GetComponent<UIController>().HideMicroscopePanel();
        GameObject.Find("SceneController").GetComponent<SceneCT>().SetCamMain();

       
    }
}
