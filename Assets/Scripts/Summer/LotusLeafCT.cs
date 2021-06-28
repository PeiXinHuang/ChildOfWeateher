using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusLeafCT : MonoBehaviour
{
  
    public LotusLeafRoadCT lotusLeafRoadCT; //荷叶路
    public Vector3 oriScale; //原始荷叶大小
    public Vector3 minScale; //最小大小
    private void Awake()
    {
        oriScale = this.transform.localScale;
        minScale = new Vector3(0.001f, 0.001f, 0.001f);
    }

    //进入Trigger，显示邻近的荷叶
    private void OnTriggerEnter(Collider other)
    {
        lotusLeafRoadCT.ShowLeafs(this);
    }

 

    //显示荷叶
    public void ShowLeaf()
    {
        this.gameObject.SetActive(true);

        this.transform.localScale = minScale;
        this.transform.LeanScale(oriScale, 0.5f).setEaseInOutBack();
    }

    //隐藏荷叶
    public void HideLeaf()
    {
        this.transform.LeanScale(minScale, 0.5f).setEaseInOutBack();
        StartCoroutine(HideSelfForSecond(0.5f));
    }

    public void HideLeafImmediate()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator HideSelfForSecond(float sencond)
    {
        yield return new WaitForSeconds(sencond);
        this.gameObject.SetActive(false);
    }


}
