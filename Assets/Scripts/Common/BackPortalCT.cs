using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPortalCT : MonoBehaviour
{
    [SerializeField] private GameObject portalChild = null;
    public GameObject backPos;
    public float speed = 5.0f;
    public PlayerCT playerCT;
    public UIController uIController;
    private void Update()
    {
        portalChild.transform.Rotate(transform.forward, speed * Time.deltaTime, Space.World);
    }

    //进入传送门，改变位置
    private void OnTriggerEnter(Collider other)
    {
        uIController.ShowFadeImg();
        playerCT.ChangePosAfterSecond(backPos);
    }
}
