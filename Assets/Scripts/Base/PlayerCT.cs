using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCT : CharacterCT
{
    
    
    #region 移动旋转动画相关
    [Header("移动旋转动画")]

    public float speed = 300;
    private CharacterController characterController;
    private Camera mainCam;

    [SerializeField] private GameObject character;
    private Animator animator;

    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 dir;

    private bool canRun = true;

    private void RotateCharacter()
    {
        if (!canRun)
            return;
        Vector3 selfPos = this.transform.position;
        Vector3 camPos = mainCam.transform.position;
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.LookAt(new Vector3((selfPos * 2 - camPos).x, this.transform.position.y, (selfPos * 2 - camPos).z));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.LookAt(new Vector3(camPos.x, selfPos.y, camPos.z));
        }
        if (Input.GetKey(KeyCode.A )|| Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 vector = selfPos + Vector3.Cross((selfPos - camPos), this.transform.up);
            this.transform.LookAt(new Vector3(vector.x, this.transform.position.y, vector.z));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 vector = selfPos - Vector3.Cross((selfPos - camPos), this.transform.up);
            this.transform.LookAt(new Vector3(vector.x, this.transform.position.y, vector.z));
        }

 
    }

    //移动
    private void MoveCharacter()
    {
        if (canRun)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float length = new Vector3(horizontal, 0, vertical).magnitude;


            characterController.SimpleMove(transform.forward * length * Time.deltaTime * speed);
            if (length > 0.0005f)
            {
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }
        }
    }

    private void JumpCharacter()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isJump", true);
            canRun = false;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            animator.SetBool("isJump", false);
            canRun = true;
        }
   
    }

  

    private void AttackCharacter(bool isAttach)
    {
        if (isAttach)
        {
            animator.SetBool("isAttack", true);
            canRun = false;
        }
        else
        {
            animator.SetBool("isAttack", false);
            canRun = true;
        }
    }


    public void SetRetSpeed(Slider slider)
    {
        speed = 100 + 500 * slider.value;
    }

    #endregion



    private void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        mainCam = Camera.main;
        animator = character.GetComponent<Animator>();
    }
    void Update()
    {
        
        JumpCharacter();
        RotateCharacter();
        MoveCharacter();
    }

    private void OnMouseDown()
    {
        //对话
        base.uiController.ShowMessage(base.characterName, base.characterText);
        AttackCharacter(true);
    }

    private void OnMouseUp()
    {
        AttackCharacter(false);
    }


    #region 改变角色位置相关
    public void DebugA(float A)
    {

    }
    public void ChangePosAfterSecond(GameObject objPos)
    {
        float second = 0.5f;
        Vector3 pos = objPos.transform.position;
        GameObject.Find("SceneController").GetComponent<UIController>().ShowFadeImg();
        StartCoroutine(ChangePos(second, pos));
    }
    private IEnumerator ChangePos(float second, Vector3 pos)
    {
        yield return new WaitForSeconds(second);
        characterController.enabled = false;
        this.transform.position = pos;
        characterController.enabled = true;
        uiController.HideSeasonPanel();
    }
    #endregion

    #region 服装相关
    [Header("服装材质相关")]
    public List<Material> clothesMats = new List<Material>();
    [SerializeField] private GameObject bodyObj = null;
    [SerializeField] private GameObject effect = null;
    public void BeginChangeClothesMat(int index)
    {
        effect.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ChangeClothesMat(index));
    }
    private IEnumerator ChangeClothesMat(int index)
    {
        yield return new WaitForSeconds(1.0f);
        bodyObj.GetComponent<Renderer>().material = clothesMats[index];
        effect.gameObject.SetActive(false);
    }
    #endregion


   
}
