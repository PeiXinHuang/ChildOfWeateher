using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SantaClausCT : NPCCT
{
    public PlayerCT playerCT;
  
    public List<FoodCT> foods = new List<FoodCT>();
    public Dictionary<FoodCT, bool> hasGetFoods = new Dictionary<FoodCT, bool>();

    public GameObject jewel;
    public GameObject btnPos;
    public Button btn;
    public GameObject chicken;

    private void Start()
    {
        InitHasGetFoods();
    }

    private void InitHasGetFoods()
    {
        foreach (FoodCT item in foods)
        {
           
            hasGetFoods.Add(item, false);
        }
    }

    //判断是否收集到所有食物
    private bool ChargeGetAllFood()
    {
        foreach (bool item in hasGetFoods.Values)
        {
           
            if (!item)
                return false;
        }
        return true;
    }



    public void SetGetFood(FoodCT foodCT)
    {
        hasGetFoods[foodCT] = true;
    }


    public void ShowJewel()
    {
       
        if (!GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasGetTask())
        {
            SayMessage("你需要先去接收任务");
            return;
        }

        if (ChargeGetAllFood())
        {
               
            if(GameObject.Find("SceneController").GetComponent<SceneCT>().GetHasFinishTask(3))
            {
                SayMessage("宝石已经给你了，不要影响我享受火鸡大餐");
                return;
            }

            jewel.SetActive(true);
            chicken.gameObject.SetActive(true);
            base.SayMessage("恭喜你已经合成了火鸡，宝石就给你吧");

        }
        else
        {
            GameObject.Find("SceneController").GetComponent<SceneCT>().LostLife();
            base.SayMessage("你还没收集完合成火鸡的材料,继续去寻找吧,看电视可以学习制作火鸡的方法");
                
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        btn.transform.position = btnPos.transform.position;
        btn.gameObject.SetActive(true);
        SayMessage("只有给我制作一道火鸡大餐，你才能够获得冬天之石");
    }

    private void OnTriggerExit(Collider other)
    {
        btn.gameObject.SetActive(false);
        EndSayMessage();
    }
}
