using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCT : MonoBehaviour
{
   
    [SerializeField] private GameObject selectObj = null; //选中的物体
    [SerializeField] private bool isMoveObj = false; //是否正在移动物体

    public List<GameObject> puzzles = new List<GameObject>();
    public List<GameObject> puzzlePos = new List<GameObject>();
    public Camera camPuzzle;

    public float distance = 0.05f;

    private GameObject GetModelSelect()
    {
        Ray ray;
        RaycastHit hitObj;
        GameObject obj = null;
        ray = camPuzzle.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitObj, 10000))
        {
            obj = hitObj.collider.gameObject;
           
            if (puzzles.Contains(obj))
                return obj;
        }
        
        return null;
    }


    private void Update()
    {




        if (Input.GetMouseButtonUp(0))
        {
            int index = GetPuzzleIndex(selectObj);
            if (index != -1)
            {
                if (Vector3.Distance(selectObj.transform.position, puzzlePos[index].transform.position) < distance)
                {
                    selectObj.transform.position = puzzlePos[index].transform.position;
                }
            }
            else
            {
                Debug.Log("没有找到对应拼图");
            }
        }

        if (!Input.GetMouseButton(0))
        {
            isMoveObj = false;
            selectObj = null;
            return;
        }

       

        if (Input.GetMouseButtonDown(0))
        {
            
            selectObj = GetModelSelect();
            isMoveObj = true;
        }

        if (isMoveObj&&selectObj)
        {
            //定义一个射线
            Ray ray = camPuzzle.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 12))
            {
                if (hit.collider.name == "PuzzlePlane")
                {

                    //selectObj.transform.position = hit.point;             
                    MoveObj(hit.point);
                }
            }
        }
    }

    private  void MoveObj(Vector3 pos)
    {
        selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, pos, Time.deltaTime*20);
    }

    //找到对应拼图的编号（0~8）
    public int GetPuzzleIndex(GameObject obj)
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            if (obj == puzzles[i])
                return i;
        }

        return -1;
    }

    public bool ChargePuzzle()
    {
        for (int i = 0; i < puzzles.Count; i++)
        {
            if (Vector3.Distance(puzzles[i].transform.position, puzzlePos[i].transform.position) > distance)
                return false;
        }
        return true;
    }
  
}
