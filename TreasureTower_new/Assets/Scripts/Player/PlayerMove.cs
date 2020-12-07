using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 4.5f;          //이동속도
    public float crouchSpeed = 2.0f;    //앉은속도
    public float rotateSpeed = 5.0f;    //회전속도
    //public float jumpPower = 5.0f;      //점프정도

    //public GameObject miniMap;

    public Player player;

    private Rigidbody characterRigidbody;   //캐릭터 Rigidbody
    private Transform characterTransform;   //캐릭터 Transform

    private Animator animator;

    private MainCamera mainCamera;

    Vector3 movement;           //이동값 받을 벡터변수
    float h, v;                 //h = horizontal, v = vertical
    public bool isJumping = false;     //점프를 했는지
    public bool isGround = true;       //캐릭터가 땅에 닿아있는지

    private void Awake()
    {
        player = GetComponent<Player>();

        characterRigidbody = GetComponent<Rigidbody>();
        characterTransform = GetComponent<Transform>();

        animator = GetComponent<Animator>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if(player.isMove && !player.isCrouch)
        {
            player.isMakeSomeNoise = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (!player.isThrow && !player.moveFreezeCheck)
        {
            Move();
            if(!player.isInteraction)
                Turn();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            player.isCrouch = !player.isCrouch;
            animator.SetBool("isCrouch", player.isCrouch);
        }
        
    }

    //=========================================
    //이동관련 함수
    void Move()
    {
        //애니메이션 체크용 플레이어 움직임 확인
        if(h > 0.0f)
        {
            player.isMove = true;
            animator.SetBool("isMove", true);
        }

        else if(h < 0.0f)
        {
            player.isMove = true;
            animator.SetBool("isMove", true);
        }

        else if(v > 0.0f)
        {
            player.isMove = true;
            animator.SetBool("isMove", true);
        }

        else if (v < 0.0f)
        {
            player.isMove = true;
            animator.SetBool("isMove", true);
        }

        else
        {
            player.isMove = false;
            player.isMakeSomeNoise = false;
            animator.SetBool("isMove", false);
        }

        
        //실제 플레이어 이동
        if (mainCamera.cameraLook == 0)
        {
            movement.Set(h, 0, v);
            //====================
            movement.Normalize();
        }

        if (player.isInteraction && player.isMove)
        {
            player.GetComponent<Animator>().SetBool("isInteraction", false);
        }

        else if (!player.isInteraction && player.isMove)
        {
            if (!player.isCrouch)
            {
                movement = Camera.main.transform.TransformDirection(movement);
                movement.y = 0;
                movement = movement * speed * Time.deltaTime;
            }

            else
            {
                movement = Camera.main.transform.TransformDirection(movement);
                movement.y = 0;
                movement = movement * crouchSpeed * Time.deltaTime;
            }

            characterRigidbody.MovePosition(transform.position + movement);
        }
    }

    void Turn()
    {
        if (h == 0 && v == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(movement);

        characterRigidbody.rotation = Quaternion.Slerp(characterRigidbody.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    /*void Jump()
    {
        if (!isJumping || !isGround)
        {
            return;
        }

        characterRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isGround = false;
        //isJumping = false;
    }*/

}
