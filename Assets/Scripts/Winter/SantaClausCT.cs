using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaClausCT : NPCCT
{

    public List<FoodCT> foods = new List<FoodCT>();
    public Dictionary<FoodCT, bool> hasGetFoods = new Dictionary<FoodCT, bool>();

    public GameObject jewel;

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

    private void OnMouseDown()
    {

        //NPC和主角的距离小于distance时候才发挥作用
        if (Vector3.Distance(this.transform.position, playerCT.transform.position) < distance)
        {
          
            
             SayMessage("只有给我制作一道火鸡大餐，你才能够获得冬天之石");
                
          
        }
    }

    public void SetGetFood(FoodCT foodCT)
    {
        hasGetFoods[foodCT] = true;
    }


    public void ShowJewel()
    {
        if (Vector3.Distance(this.transform.position, playerCT.transform.position) < distance)
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
                base.SayMessage("恭喜你已经合成了火鸡，宝石就给你吧");
            }
            else
            {
                base.SayMessage("你还没收集完合成火鸡的材料");
            }
        }
    }
}
