using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCT : CharacterCT
{
    private void OnTriggerEnter(Collider other)
    {
        SayMessage(base.characterText);
    }

    private void OnTriggerExit(Collider other)
    {
        EndSayMessage();
    }

    public void SayMessage(string _text)
    {
        base.uiController.ShowMessage(base.characterName, _text);
    }

    public void EndSayMessage()
    {
        base.uiController.HideMessageImmediate();
    }
}
