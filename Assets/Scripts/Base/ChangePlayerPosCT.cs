using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerPosCT : MonoBehaviour
{
    public PlayerCT playerCT;
    public UIController uIController;
    public List<GameObject> SeasonPos = new List<GameObject>();

    public void SetPlayerPos(int seasonIndex)
    {

        uIController.ShowFadeImg();
        playerCT.GetComponent<CharacterController>().transform.position = SeasonPos[seasonIndex].transform.position;

    }
}
