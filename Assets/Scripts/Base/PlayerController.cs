using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float maxAngleSpeed = 400;//最大角速度
    public float minAngleSpeed = 100;//最小角速度
    public float acceleration = 40;//加速度
    public float maxMoveSpeed = 5f;//最大移动速度
    public float jumpSpeed = 10f;//起跳速度
    public float gravity = 10f;//重力大小
    public Transform cameraTransform;//相机的Transform


    bool isOnGround;//是否在地面上
    bool isSecondJump;//是否跳过第二次
    float ySpeed;//竖直方向的速度值
    float moveSpeed;//移动速度值
    Vector3 input;//输入
    Vector3 move;//速度
    CharacterController cont;

    void Start()
    {
        cont = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        isOnGround = cont.isGrounded;
        Drop();
        Jump();
        Turn();
        Move();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        input.x = h;
        input.z = v;

        moveSpeed = Mathf.MoveTowards
            (moveSpeed, input.normalized.magnitude * maxMoveSpeed, acceleration * Time.deltaTime);

        //前后左右移动
        //设置移动向量（相对坐标系下的）
        move = input * Time.deltaTime * moveSpeed;
        //相对坐标系转为世界坐标系
        move = this.cameraTransform.TransformDirection(move);

        //下落
        //Δy = Vy * Δt
        move += Vector3.up * ySpeed * Time.deltaTime;


        //【与下面代码等效】cont.Move(transform.forward * v + transform.right * h);
        cont.Move(move);
    }

    void Drop()
    {
        if (!isOnGround)
        {
            //ΔV = g * Δt
            ySpeed -= gravity * Time.deltaTime;

        }
        else//在地面
        {
            if (ySpeed < -1)//地面给的支持力
            {
                ySpeed += gravity * Time.deltaTime;
            }

            if (isSecondJump)
            {
                isSecondJump = false;
            }
        }
    }

    void Jump()
    {
        if (isOnGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            if (!isSecondJump)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    ySpeed += jumpSpeed;
                    isSecondJump = true;
                }
            }
        }
    }

    void Turn()
    {
        if (input.x != 0 || input.z != 0)//移动时
        {
            //获取目标方向的四元数
            Vector3 targetDirection = cameraTransform.TransformDirection(input);
            targetDirection.y = 0;
            Quaternion lookQuaternion = Quaternion.LookRotation(targetDirection);

            //根据移动速度计算转向速度
            float turnSpeed = Mathf.Lerp(minAngleSpeed, maxAngleSpeed, moveSpeed / maxMoveSpeed);

            transform.rotation = Quaternion.RotateTowards
               (transform.rotation, lookQuaternion, turnSpeed * Time.deltaTime);

        }
    }
}

