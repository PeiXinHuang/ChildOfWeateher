using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCT : NPCCT
{



    private void OnTriggerEnter(Collider other)
    {
        base.SayMessage(base.characterText);
    }

    private void OnTriggerExit(Collider other)
    {
        base.EndSayMessage();
    }

}
