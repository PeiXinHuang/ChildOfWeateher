using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCT : MonoBehaviour
{
    public Transform target;//主相机要围绕其旋转的物体 
    public float distance = 7.0f;//主相机与目标物体之间的距离 
    private float eulerAngles_x;
    private float eulerAngles_y;

    //水平滚动相关 
    public int distanceMax = 10;//主相机与目标物体之间的最大距离 
    public int distanceMin = 3;//主相机与目标物体之间的最小距离 
    public float xSpeed = 70.0f;//主相机水平方向旋转速度 

    //垂直滚动相关 
    public int yMaxLimit = 60;//最大y（单位是角度） 
    public int yMinLimit = -10;//最小y（单位是角度） 
    public float ySpeed = 70.0f;//主相机纵向旋转速度 

    public float oriDistance = 5.0f;

    //滚轮相关 
    public float MouseScrollWheelSensitivity = 1.0f;//鼠标滚轮灵敏度（备注：鼠标滚轮滚动后将调整相机与目标物体之间的间隔） 
    public LayerMask CollisionLayerMask;

    void Start()
    {
        Vector3 eulerAngles = this.transform.eulerAngles;//当前物体的欧拉角 
        this.eulerAngles_x = eulerAngles.y;
        this.eulerAngles_y = eulerAngles.x;
    }

    void LateUpdate()
    {
        if (this.target != null)
        {
            this.eulerAngles_x += ((Input.GetAxis("Mouse X") * this.xSpeed) * this.distance) * 0.02f;
            this.eulerAngles_y -= (Input.GetAxis("Mouse Y") * this.ySpeed) * 0.02f;
            this.eulerAngles_y = ClampAngle(this.eulerAngles_y, (float)this.yMinLimit, (float)this.yMaxLimit);
            this.oriDistance = Mathf.Clamp(this.oriDistance - (Input.GetAxis("Mouse ScrollWheel") * MouseScrollWheelSensitivity), (float)this.distanceMin, (float)this.distanceMax);


            Quaternion quaternion = Quaternion.Euler(this.eulerAngles_y, this.eulerAngles_x, (float)0);



            //从目标物体处，到当前脚本所依附的对象（主相机）发射一个射线，如果中间有物体阻隔，则更改this.distance（这样做的目的是为了不被挡住） 

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Linecast(this.target.position, this.transform.position, out hitInfo, this.CollisionLayerMask))
            {
                // Debug.Log(hitInfo.collider.name);

                this.distance = hitInfo.distance+0.05f;
               // this.distance = Mathf.Max(this.distance, distanceMin);
               
            }

            else
            {
                this.distance = oriDistance;
            }
            
            Vector3 vector = ((Vector3)(quaternion * new Vector3((float)0, (float)0, -this.distance))) + this.target.position;

            //更改主相机的旋转角度和位置
            this.transform.rotation = quaternion;
            this.transform.position = vector;
       
        }



      


    }

    //把角度限制到给定范围内 
    public float ClampAngle(float angle, float min, float max)
    {
        while (angle < -360)
        {
            angle += 360;
        }
        while (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }


    public Vector3 GetAngle()
    {
        Vector3 beginPos = target.position;
        Vector3 endPos = target.position + new Vector3(0,10,10);

        Vector3 pos1 = Vector3.Lerp(beginPos, endPos, 0.25f);
        Vector3 pos2 = Vector3.Lerp(beginPos, endPos, 0.5f);
        Vector3 pos3 = Vector3.Lerp(beginPos, endPos, 0.75f);
        Vector3[] posArray = new Vector3[] { beginPos, pos1, pos2, pos3, endPos };
        Vector3 TargetPos = posArray[0];

        for (int i = 0; i < 5; i++)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(posArray[i], target.position - posArray[i], out hitinfo))
            {
                if (hitinfo.collider.tag == "wall")
                {
                    TargetPos = posArray[i];
                }
                else
                {
                    break;
                }
            }
        }

        return TargetPos;

    }

    public void SetSpeed(Slider slider)
    {
        xSpeed = 20 + 50 * slider.value;
        ySpeed = 20 + 50 * slider.value;
    }

}
