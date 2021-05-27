using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusLeafRoadCT : MonoBehaviour
{
    public List<LotusLeafCT> leafs = new List<LotusLeafCT>();

    [SerializeField]private LotusLeafCT previousLeaf = null;
    [SerializeField]private LotusLeafCT currentLeaf = null;
    [SerializeField]private LotusLeafCT nextLeaf = null;


    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            leafs.Add(this.transform.GetChild(i).GetComponent<LotusLeafCT>());
        }
      
    }

    //显示邻近的荷叶,隐藏之前的荷叶
    public void ShowLeafs(LotusLeafCT leaf)
    {

        currentLeaf = leaf;
        int index = leafs.IndexOf(leaf);
        //往前走,隐藏后面的荷叶，显示前面的荷叶
        if (currentLeaf == nextLeaf)
        {
            if (previousLeaf) previousLeaf.HideLeaf();
            //存在下一片荷叶，显示荷叶,不存在，将下一片荷叶置为空
            if (index + 1 < leafs.Count)
            {
                nextLeaf = leafs[index + 1];
                nextLeaf.ShowLeaf();
            }
            else
            {
                nextLeaf = null;
            }

            if (index - 1 >= 0)
            {
                previousLeaf = leafs[index - 1];
            }
            else
            {
                previousLeaf = null;
            }
        }

        //往后走
        if(currentLeaf == previousLeaf)
        {
            if (nextLeaf) nextLeaf.HideLeaf();
            if (index - 1 >= 0)
            {              
                previousLeaf = leafs[index - 1];
                previousLeaf.ShowLeaf();
            }
            else
            {
                previousLeaf = null;
            }

            if (index + 1 < leafs.Count)
            {
                nextLeaf = leafs[index + 1];
            }
            else
            {
                nextLeaf = null;
            }
        }
    }

    //重置荷叶路
    public void ResetRoad()
    {

        if(previousLeaf) previousLeaf.HideLeaf();
        if(currentLeaf) currentLeaf.HideLeaf();
        if (nextLeaf) nextLeaf.HideLeaf();

        previousLeaf = null;
        nextLeaf = leafs[0];
        currentLeaf = leafs[0];

        currentLeaf.ShowLeaf();

    }

    

}
