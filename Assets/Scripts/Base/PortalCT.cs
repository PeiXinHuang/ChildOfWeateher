using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCT : MonoBehaviour
{
    public UIController uIController;
    public bool canShowSeasonPanel = false;
   
    private void OnTriggerEnter(Collider other)
    {
        canShowSeasonPanel = true;
        uIController.ShowMessage("", "回车打开季节切换面板");
    }

    private void OnTriggerExit(Collider other)
    {
        canShowSeasonPanel = false;
        uIController.HideSeasonPanel();
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return)) && canShowSeasonPanel)
        {
            uIController.ShowSeasonPanel();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            uIController.HideSeasonPanel();
        }
    }

}
