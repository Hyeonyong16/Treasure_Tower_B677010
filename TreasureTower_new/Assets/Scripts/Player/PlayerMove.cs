using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;          //이동속도
    public float rotateSpeed = 5.0f;    //회전속도
    public float jumpPower = 5.0f;      //점프정도

    //public GameObject miniMap;

    private Rigidbody characterRigidbody;   //캐릭터 Rigidbody
    private Transform characterTransform;   //캐릭터 Transform

    private MainCamera mainCamera;

    Vector3 movement;           //이동값 받을 벡터변수
    float h, v;                 //h = horizontal, v = vertical
    public bool isJumping = false;     //점프를 했는지
    public bool isGround = true;       //캐릭터가 땅에 닿아있는지

    private void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody>();
        characterTransform = GetComponent<Transform>();
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
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    //=========================================
    //이동관련 함수
    void Move()
    {
        if (mainCamera.cameraLook == 0)
        {
            movement.Set(h, 0, v);
        }
        else if (mainCamera.cameraLook == 1)
        {
            movement.Set(v, 0, -h);
        }
        else if (mainCamera.cameraLook == 2)
        {
            movement.Set(-h, 0, -v);
        }
        else
        {
            movement.Set(-v, 0, h);
        }

        movement = movement.normalized * speed * Time.deltaTime;

        characterRigidbody.MovePosition(transform.position + movement);
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
