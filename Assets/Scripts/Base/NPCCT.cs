using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCT : CharacterCT
{
    public PlayerCT playerCT;
    public float distance = 10.0f; 

    private void OnMouseDown()
    {
        //NPC和主角的距离小于distance时候才发挥作用dd
        if (Vector3.Distance(this.transform.position,playerCT.transform.position)<distance)
            base.uiController.ShowMessage(base.characterName, base.characterText);
    }


    public void SayMessage(string _text)
    {
        base.uiController.ShowMessage(base.characterName, _text);
    }
}
