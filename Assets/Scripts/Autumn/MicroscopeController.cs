using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MicroscopeController : MonoBehaviour
{




    public Camera camMicroscope;
    private RaycastHit hitObj;
    private Ray ray;
    private GameObject selectObj = null; //选中的物体



    //获取选中的物体
    private GameObject GetModelSelect()
    {
        GameObject obj = null;
        ray = camMicroscope.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitObj, 10000))
            obj = hitObj.collider.gameObject;
        return obj;
    }



    public MicroscopeUIController microscopeUIController;

    //可以选中的部件
    [Header("模型部件")]
    [SerializeField] private GameObject jingTou = null;
    [SerializeField] private GameObject xuanNiuLB = null;
    [SerializeField] private GameObject xuanNiuLS = null;
    [SerializeField] private GameObject xuanNiuRB = null;
    [SerializeField] private GameObject xuanNiuRS = null;
    [SerializeField] private GameObject jingZi = null;
    [SerializeField] private GameObject jingTong = null;
    [SerializeField] private GameObject jiaPianLeft = null;
    [SerializeField] private GameObject jiaPianRight = null;
    [SerializeField] private GameObject guanQuan = null;

    [Header("镜头旋转相关")]
    public float jingTouSpeed = 30.0f; //镜头旋转速度
    //3个镜头的位置
    [SerializeField] private List<GameObject> jingTouPos = new List<GameObject>();
    //镜头发挥作用的位置
    [SerializeField] private GameObject jingTouPlayPos = null;

    public int jingTouIndex = 0; //发挥作用的镜头

    public float jingTouPlayDis = 5.0f;//镜头发挥作用的距离

    [Header("旋钮旋转速度")]
    public float xuanNiuSpeed = 30.0f; //旋钮旋转速度

    [Header("镜子旋转速度")]
    public float jingZiSpeed = 10.0f; //镜子旋转速度
    public float jingZiMaxAngle = 25.0f; //镜子最大旋转角度
    public float jingZiMinAngle = -25.0f; //镜子最小旋转角度
    public float jingZiAngle = 0.0f; //镜子旋转角度
    public float jingZiBestAngle = 0.0f; //镜子最优旋转角度

    [Header("图片缩放速度")]
    public float amplificationBSpeed = 0.1f; //显微镜中图片的放大速度一，由大旋纽控制
    public float amplificationSSpeed = 0.01f; //显微镜中图片的放大速度二，由小旋纽控制
    public float maxAmplification = 5.0f; //显微镜中图片的最大最小倍数
    public float minAmplification = 1.0f;
    public float amplification = 1.0f; //显微镜中图片的放大倍数
    public float bestAmplification = 2.0f; //图片的最优倍数

    [Header("图片亮度")]
    public float imgIdensity = 1.0f; //图片亮度
    public float imgMaxIdensity = 1.0f; //图片最大亮度
    public float imgMinIdensity = 0.5f; //图片最小亮度
    public float imgIdensitySpeed = 0.1f; //图片亮度调节速度
    public float imgBestIdensity = 1.0f; //图片的最优亮度

    [Header("图片模糊程度")]
    public float imgBurl = 0.0f; //图片模糊程度
    public float imgMaxBurl = 10.0f; //图片最大模糊程度
    public float imgMinBurl = 0.0f; //图片最小模糊程度
    public float imgBurlSpeed = 0.1f; //图片模糊程度速度改变
    public float imgBestBurl = 0.0f; //图片最优模糊程度

    [Header("镜筒高度")]
    public float jingTongHeight = 0.0f; //镜筒高度
    public float jingTongMaxHeight = 0.0f; //镜筒最高高度
    public float jingTongMinHeight = -0.3f; //镜筒最底高度
    public float jingTongBSpeed = 0.005f; //镜筒移动速度1
    public float jingTongSSpeed = 0.002f; //镜筒移动速度2
    public float jingTongBestHeight = -0.2f; //镜筒的最优高度

    [Header("夹片角度")]
    //左夹片
    public float jiaPianLeftAngle = 0.0f; //夹片角度
    public float jiaPianLeftMaxAngle = 0.0f; //夹片最大角度
    public float jiaPianLeftMinAngle = -25.0f; //夹片最小角度
    //右夹片
    public float jiaPianRightAngle = 25.0f; //夹片角度
    public float jiaPianRightMaxAngle = 0.0f; //夹片最大角度
    public float jiaPianRightMinAngle = 0.0f; //夹片最小角度
    public float jiaPianSpeed = 10.0f; //夹片移动速度

    [Header("光圈相关")]
    public float guanQuanAngle = 0.00f; //光圈角度
    public float guanQuanSpeed = 10.0f; //光圈移动速度

    [Header("载玻片相关")]
    [SerializeField] private GameObject zaiBoPianPos = null;//载玻片位置
    public GameObject zaiBoPian = null; //载玻片
    public bool hasZaiBoPian = false;



    private Vector2 mouseOriPos = Vector2.zero; //鼠标第一次点击位置
    private Vector2 modelCenterPos = Vector2.zero; //模型中心位置
    private Vector2 pressVector2 = Vector2.zero; //鼠标第一次点击和模型中心位置之间的向量


    //选中UI后，计算旋转过的角度
    private float angle = 0;
    private Vector2 oldVector2 = Vector2.zero;
    private Vector2 newVector2 = Vector2.zero;
    private bool isRotateUI = false; //是否正在旋转UI



    private void Update()
    {


        //鼠标按下，如果选中显微镜部件，显示旋转UI
        if (Input.GetMouseButtonDown(0))
        {
            selectObj = GetModelSelect();

            if (selectObj == jingTou || selectObj == xuanNiuLB || selectObj == xuanNiuLS ||
                selectObj == xuanNiuRB || selectObj == xuanNiuRS || selectObj == jingZi
                || selectObj == jiaPianLeft || selectObj == jiaPianRight || selectObj == guanQuan)
            {
                if (!microscopeUIController.GetRotateUIShow()) //第一次点击，显示旋转UI
                {
                    //关闭模型旋转
                    this.GetComponent<ModelRotation>().enabled = false;

                    InitRotateUI();

                    isRotateUI = true; //开始旋转UI
                }

            }


        }



        if (Input.GetMouseButtonUp(0))
        {

            //鼠标抬起，重置旋转UI
            if (isRotateUI)
            {

                microscopeUIController.ResetRotateUI();
                isRotateUI = false;
                angle = 0;

                if (hasZaiBoPian)
                    this.GetComponent<ModelRotation>().enabled = true;
            }



        }


        //根据选中模型，对UI进行旋转，对显微镜模型进行操作
        if (isRotateUI)
        {
            //UI旋转
            float angleTemp = RotateUI();

            //对模型部件进行旋转，并对显微镜显示内容进行修改
            SetJingTouRotate(angleTemp);
            SetXuanNiuRotate(angleTemp);
            SetJingTongHeight(angleTemp);
            SetJingZiAngle(angleTemp);
            SetJiaPianAngle(angleTemp);
            SetGuanQuan(angleTemp);
            SetImgIdensity(angleTemp);
            SetImgBurl(angleTemp);
        }
    }

    //初始化旋转UI
    private void InitRotateUI()
    {

        //设置旋转UI位置并显示
        //获取模型中心点，设置RotateUI显示位置
        //获取鼠标位置和模型中心点形成的向量，设置RotateUI的开始旋转位置

        mouseOriPos = Input.mousePosition;
        modelCenterPos = camMicroscope.WorldToScreenPoint(selectObj.transform.position);
        Vector2 pressVector2 = mouseOriPos - modelCenterPos;

        microscopeUIController.SetRotateUIPos(modelCenterPos, Quaternion.FromToRotation(Vector3.up, new Vector3(pressVector2.x, pressVector2.y)));
        microscopeUIController.SetRotateUIShow(true);

        oldVector2 = pressVector2;
    }

    //开始进行UI旋转
    private float RotateUI()
    {
        //计算鼠标位置和模型中心点，设置RotateChildUI宽度
        //计算鼠标位置和模型中心点形成的向量和第一次按下鼠标和模型中心点形成的向量之间的夹角，设置RotateChildUI旋转的角度

        newVector2 = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - modelCenterPos;

        float angleTemp = Vector2.Angle(oldVector2, newVector2);
        Vector3 normal = Vector3.Cross(oldVector2, newVector2);
        float dir = Mathf.Sign(Vector3.Dot(normal, Vector3.back));
        angle += dir * angleTemp;

        oldVector2 = newVector2;


        microscopeUIController.SetRotateUIFill(angle);

        float width = (newVector2).magnitude;
        microscopeUIController.SetRotateUIWidth(width);

        return angleTemp * dir;
    }

    //镜头旋转设置,获取当前镜头
    private void SetJingTouRotate(float angleTemp)
    {
       
        if (selectObj == jingTou)
        {
            jingTou.gameObject.transform.Rotate(jingTou.transform.up, angleTemp * Time.deltaTime * jingTouSpeed * -1, Space.World);
            SetJingTou();
        }


    }

    //设置镜子旋转角度
    private void SetJingZiAngle(float angleTemp)
    {
        if (selectObj == jingZi)
        {

            //selectObj.gameObject.transform.Rotate(selectObj.transform.forward, angleTemp * Time.deltaTime * jingZiSpeed * -1, Space.World);

            jingZiAngle -= angleTemp * Time.deltaTime * jingZiSpeed;
            jingZiAngle = Mathf.Max(jingZiAngle, jingZiMinAngle);
            jingZiAngle = Mathf.Min(jingZiAngle, jingZiMaxAngle);
            selectObj.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, jingZiAngle));

        }

    }

    //设置显微镜图片亮度内容
    private void SetImgIdensity(float angleTemp)
    {
        //设置图片的大小
        if (selectObj == jingTou)
        {
        }
        else if (selectObj == xuanNiuLB)
        {
            amplification += angleTemp * Time.deltaTime * amplificationBSpeed;

        }
        else if (selectObj == xuanNiuLS)
        {
            amplification += angleTemp * Time.deltaTime * amplificationSSpeed;

        }
        else if (selectObj == xuanNiuRB)
        {
            amplification -= angleTemp * Time.deltaTime * amplificationBSpeed;

        }
        else if (selectObj == xuanNiuRS)
        {
            amplification -= angleTemp * Time.deltaTime * amplificationSSpeed;

        }
     
        amplification = Mathf.Min(maxAmplification, amplification);
        amplification = Mathf.Max(minAmplification, amplification);
        microscopeUIController.SetImageAmplification(amplification);


        //设置图片的亮度
        float temp = 1 - Mathf.Abs(jingZiAngle) / 100;
        float angle = (guanQuanAngle + 36000) % 360;
        float idensity = 0f;
        if ((0 <= angle && angle <= 36) || (360 - 36 <= angle && angle < 360))
            idensity = 1;
        else if (36 < angle && angle <= 36 * 3)
            idensity = 2;
        else if (36 * 3 < angle && angle <= 36 * 5)
            idensity = 3;
        else if (36 * 5 < angle && angle <= 36 * 7)
            idensity = 4;
        else if (36 * 7 < angle && angle <= 36 * 9)
            idensity = 5;
        imgIdensity = idensity / 5 * temp;
        imgIdensity = Mathf.Min(imgMaxIdensity, imgIdensity);
        imgIdensity = Mathf.Max(imgMinIdensity, imgIdensity);
        microscopeUIController.SetImageIdensity(imgIdensity);
    }

    //设置旋钮旋转
    private void SetXuanNiuRotate(float angleTemp)
    {
        if (selectObj == xuanNiuLB)
        {
            selectObj.gameObject.transform.Rotate(selectObj.transform.right, angleTemp * Time.deltaTime * xuanNiuSpeed, Space.World);
        }
        else if (selectObj == xuanNiuLS)
        {
            selectObj.gameObject.transform.Rotate(selectObj.transform.right, angleTemp * Time.deltaTime * xuanNiuSpeed, Space.World);
        }
        else if (selectObj == xuanNiuRB)
        {
            selectObj.gameObject.transform.Rotate(selectObj.transform.right, angleTemp * Time.deltaTime * xuanNiuSpeed * -1, Space.World);
        }
        else if (selectObj == xuanNiuRS)
        {
            selectObj.gameObject.transform.Rotate(selectObj.transform.right, angleTemp * Time.deltaTime * xuanNiuSpeed * -1, Space.World);
        }
    }


    //设置镜筒高度
    private void SetJingTongHeight(float angleTemp)
    {

        if (selectObj == xuanNiuLB)
        {
            jingTongHeight -= angleTemp * Time.deltaTime * jingTongBSpeed;
        }
        else if (selectObj == xuanNiuLS)
        {
            jingTongHeight -= angleTemp * Time.deltaTime * jingTongSSpeed;
        }
        else if (selectObj == xuanNiuRB)
        {
            jingTongHeight += angleTemp * Time.deltaTime * jingTongBSpeed;
        }
        else if (selectObj == xuanNiuRS)
        {
            jingTongHeight += angleTemp * Time.deltaTime * jingTongSSpeed;
        }

        jingTongHeight = Mathf.Min(jingTongHeight, jingTongMaxHeight);
        jingTongHeight = Mathf.Max(jingTongHeight, jingTongMinHeight);

        jingTong.gameObject.transform.localPosition = new Vector3(0, jingTongHeight, 0);
    }

    //设置图片模糊
    private void SetImgBurl(float angleTemp)
    {
        if (selectObj == xuanNiuLB)
        {
            imgBurl = imgMaxBurl;
        }
        else if (selectObj == xuanNiuRB)
        {
            imgBurl = imgMaxBurl;
        }
        else if (selectObj == xuanNiuLS)
        {
            imgBurl -= angleTemp * Time.deltaTime * imgBurlSpeed;
        }
        else if (selectObj == xuanNiuRS)
        {
            imgBurl -= angleTemp * Time.deltaTime * imgBurlSpeed;
        }

        imgBurl = Mathf.Max(imgMinBurl, imgBurl);
        imgBurl = Mathf.Min(imgMaxBurl, imgBurl);

        microscopeUIController.SetImageBurl(imgBurl);
    }


    //设置夹片角度
    private void SetJiaPianAngle(float angleTemp)
    {

        if (selectObj == jiaPianLeft)
        {
            jiaPianLeftAngle += angleTemp * Time.deltaTime * jiaPianSpeed;
            jiaPianLeftAngle = Mathf.Max(jiaPianLeftAngle, jiaPianLeftMinAngle);
            jiaPianLeftAngle = Mathf.Min(jiaPianLeftAngle, jiaPianLeftMaxAngle);
            selectObj.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, jiaPianLeftAngle, 0));
        }
        else if (selectObj == jiaPianRight)
        {
            jiaPianRightAngle += angleTemp * Time.deltaTime * jiaPianSpeed;
            jiaPianRightAngle = Mathf.Max(jiaPianRightAngle, jiaPianRightMinAngle);
            jiaPianRightAngle = Mathf.Min(jiaPianRightAngle, jiaPianRightMaxAngle);
            selectObj.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, jiaPianRightAngle, 0));
        }
    }

    //设置光圈
    private void SetGuanQuan(float angleTemp)
    {
        if(selectObj == guanQuan)
        {
            guanQuanAngle += angleTemp * Time.deltaTime * guanQuanSpeed;
            selectObj.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, guanQuanAngle, 0));
        }
    }


    //设置起作用的镜头,并设置图片
    public void SetJingTou()
    {
        int num = 0;
       
        foreach (GameObject item in jingTouPos)
        {
            if (Vector3.Distance(item.transform.position, jingTouPlayPos.transform.position) < jingTouPlayDis)
            {
                num = jingTouPos.IndexOf(item) + 1;
            }

        }


        //切换镜头，设置模糊程度
        if (jingTouIndex != num)
        {
            imgBurl = imgMaxBurl;
        }

        jingTouIndex = num;
        microscopeUIController.SetImageIndex(num);
    }

    //设置载玻片位置
    public Vector3 GetZaiBoPianPos()
    {
        return zaiBoPianPos.transform.position;

    }




    //将镜头调到最合适
    public void SetBestJingTou(int index)
    {

        jingTongHeight = jingTongBestHeight;
        jingZiAngle = jingZiBestAngle;

        jingTong.gameObject.transform.DOLocalMoveY(jingTongHeight, 0.5f);

        jingZi.gameObject.transform.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, jingZiAngle)), 0.5f);

        jingTou.transform.rotation = Quaternion.Euler(jingTou.transform.rotation.eulerAngles.x,
            (jingTou.transform.rotation.eulerAngles.y + 36000) % 360, jingTou.transform.rotation.eulerAngles.z);

        if (index == 1)
        {
            jingTou.transform.DOLocalRotateQuaternion(Quaternion.Euler(21.0f, 0.0f, 0.0f), 0.5f);
        }
        else if (index == 2)
        {
            jingTou.transform.DOLocalRotateQuaternion(Quaternion.Euler(-6.689f, -112.4f, -20.0f), 0.5f);
        }
        else if (index == 3)
        {
            jingTou.transform.DOLocalRotateQuaternion(Quaternion.Euler(-10.5f, 114.375f, 18.27f), 0.5f);
        }



        //复原图片
        microscopeUIController.SetImageIndex(index);
        amplification = bestAmplification;
        imgIdensity = imgBestIdensity;
        imgBurl = imgBestBurl;
        microscopeUIController.SetImageAmplification(amplification);
        microscopeUIController.SetImageIdensity(imgIdensity);
        microscopeUIController.SetImageBurl(imgBurl);

    }



  
    //设置载玻片
    public void SetZaiBoPian(bool isShow)
    {
        hasZaiBoPian = isShow;
        zaiBoPian.gameObject.SetActive(isShow);

    }


}