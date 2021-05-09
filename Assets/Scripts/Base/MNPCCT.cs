using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNPCCT : CharacterCT
{
    public PlayerCT playerCT;
    public float distance = 10.0f;
    [SerializeField] private GameObject floatPanelPos = null;
    private Camera camMain;

    private void Start()
    {
        camMain = Camera.main;
    }

    public void ShowMessage()
    {
        base.uiController.ShowMessage(base.characterName, base.characterText);
    }


    private void Update()
    {

        Vector3 vector3 = camMain.WorldToScreenPoint(floatPanelPos.transform.position);
        uiController.SetFloatPanelPos(vector3);
        if (Vector3.Distance(this.transform.position, playerCT.transform.position) < distance)
        {
            uiController.ShowFloatPanel();
        }
        else
        {
            uiController.HideFloatPanel();
        }
        
    }

}
