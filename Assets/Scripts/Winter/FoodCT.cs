using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCT : MonoBehaviour
{
    public string foodName;

    public PlayerCT player;

    public float Distance = 5.0f;

    public GameObject particle;

    public SantaClausCT santaClausCT;

    public void OnMouseDown()
    {
        if(Vector3.Distance(player.transform.position,this.transform.position)<Distance)
        {
            StartCoroutine(HideObj());
        }


    }

    public IEnumerator HideObj()
    {
        particle.SetActive(true);
        player.uiController.ShowMessage(player.characterName, "已经收集到食材——" + foodName);
        LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), 0.5f);
        yield return new WaitForSeconds(2.0f);
        santaClausCT.SetGetFood(this);
        this.gameObject.SetActive(false);
       

    }
}
