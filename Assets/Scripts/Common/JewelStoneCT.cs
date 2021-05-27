using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelStoneCT : MonoBehaviour
{

    public float speed = 5.0f;
    public int stoneIndex;

    public PlayerCT playerCT;
    public float distance = 5.0f;


    void Update()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * speed, Space.Self);
    }

    private void OnMouseDown()
    {
        if (Vector3.Distance(playerCT.transform.position, this.transform.position) < distance){
            GameObject.Find("SceneController").GetComponent<SceneCT>().SetFinshTask(stoneIndex);
            HideJewelStone();
            GameObject.Find("SceneController").GetComponent<SceneCT>().ChargeFinshAllTask();
        }

    }

    private void Start()
    {
        ShowJewelStone();
    }

    private void ShowJewelStone()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        LeanTween.scale(this.gameObject, new Vector3(1, 1, 1), 1.0f).setEaseInSine();
    }

    private void HideJewelStone()
    {
        LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), 1.0f).setEaseInSine();
        StartCoroutine(HideSelf());
    }

    public IEnumerator HideSelf()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }

}
