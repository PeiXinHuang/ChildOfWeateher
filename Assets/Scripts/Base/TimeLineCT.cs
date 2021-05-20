using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineCT : MonoBehaviour
{
    public PlayerCT playerCT;
    private void Start()
    {
        StartCoroutine(HidePlayer());
    }
    public IEnumerator HidePlayer()
    {
        yield return new WaitForSeconds(1.1f);
        playerCT.transform.GetChild(0).gameObject.SetActive(false);
    }
}
