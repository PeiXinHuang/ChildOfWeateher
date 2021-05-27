using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerLoseCT : MonoBehaviour
{

    public LotusLeafRoadCT LotusLeafRoadCT;
    public PlayerCT playerCT;
    [SerializeField] private GameObject startPos = null;//复活点
    [SerializeField] private FrogCT frogCT = null;

    //掉入水中，生命减一
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("SceneController").GetComponent<SceneCT>().LostLife();
        if (GameObject.Find("SceneController").GetComponent<SceneCT>().GetLifeNum() > 0)
        {


            playerCT.GetComponent<CharacterController>().enabled = false;
            playerCT.transform.position = startPos.transform.position;
            playerCT.GetComponent<CharacterController>().enabled = true;
            frogCT.SayMessage("注意不要掉到水中，不然会减生命值的");

            LotusLeafRoadCT.ResetRoad();
        }
    }
}
