using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeUIController : MonoBehaviour
{

    #region 旋转UI相关
    [SerializeField]
    private GameObject rotateUI = null;
    [SerializeField]
    private GameObject rotateChildUI = null;

    [SerializeField]
    private GameObject OutSideCanvas = null;

    public void SetRotateUIShow(bool isShow)
    {
       
        if (isShow)
        {
            rotateUI.SetActive(true);

            
        }
        else
        {
            rotateUI.SetActive(false);
        }
    }


    public bool GetRotateUIShow()
    {
        return rotateUI.activeInHierarchy;
    }
    public void SetRotateUIPos(Vector2 pos,Quaternion quaternion)
    {
        rotateUI.transform.position = pos;
        rotateUI.transform.rotation = quaternion;
    }
    public void SetRotateUIFill(float angle)
    {
        float fillAmount = 0;
        if (angle < 0)
        {
            rotateChildUI.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            fillAmount = 1 - ((angle += 360000) % 360)/360;
        }

        else
        {
            rotateChildUI.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            fillAmount = angle % 360 / 360;
        }
       
        rotateChildUI.GetComponent<Image>().fillAmount = fillAmount;
    }
    public void SetRotateUIWidth(float width)
    {
        //rotateChildUI.GetComponent<RectTransform>().sizeDelta = new Vector2(
            //width*2/ OutSideCanvas.GetComponent<RectTransform>().sizeDelta.x, width* 2/OutSideCanvas.GetComponent<RectTransform>().sizeDelta.y);
        rotateChildUI.GetComponent<RectTransform>().sizeDelta = new Vector2(
            width * 2/ OutSideCanvas.GetComponent<RectTransform>().localScale.x, width * 2 / OutSideCanvas.GetComponent<RectTransform>().localScale.y);
    }

    public void ResetRotateUI()
    {
        SetRotateUIShow(false);
    }
    #endregion


    #region 镜头显示相关
    [SerializeField] List<Image> images = new List<Image>();

    public bool hasZaiBoPian = false; //是否有载玻片
    public int currentImageIndex = 0;

    //设置当前显示图片
    public void SetImageIndex(int index)
    {
      
       

        currentImageIndex = index;
        //没有载玻片,显示默认图片
        if (!hasZaiBoPian)
        {
            foreach (Image image in images)
            {
                image.transform.parent.gameObject.SetActive(false);
            }
            images[0].transform.parent.gameObject.SetActive(true);
        }
        else //有载玻片，显示对应的图片
        {
            foreach (Image image in images)
            {
                image.transform.parent.gameObject.SetActive(false);
            }
            images[currentImageIndex].transform.parent.gameObject.SetActive(true);
        }
        
    }

    //设置当前观察到的图片放大倍数
    public void SetImageAmplification(float amplification)
    {
        images[currentImageIndex].transform.localScale = new Vector3(amplification, amplification);
    }

    //设置图片亮度
    public void SetImageIdensity(float idensity)
    {
        images[currentImageIndex].color = new Color(idensity, idensity, idensity);
    }


    //设置图片模糊程度
    public void SetImageBurl(float burl)
    {
    
        images[currentImageIndex].materialForRendering.SetFloat("_Size", burl);

    }
    #endregion
}
