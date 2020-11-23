using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float offsetX;       //x좌표 얼만큼 더
    public float offsetY;       //y좌표 얼만큼 더
    public float offsetZ;       //z좌표 얼만큼 더

    public int cameraLook = 0;  //0이 기본, 1은 90도 회전, 2는 180도, 3은 270도

    public GameObject player;   //플레이어 오브젝트
    //public GameObject map;      //맵 만드는 카메라         rotation x 90 포지션 y 50 쯤 잡고 맵에 따라서 위치 조정

    Vector3 cameraPosition;     //변환된 카메라 포지션을 저장해둘 변수
    float cameraRotation;

    private void Start()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;

        cameraRotation = 0;
        cameraLook = 0;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    cameraRotation += 90.0f;
        //    Quaternion rotation = Quaternion.Euler(45, cameraRotation, 0);

        //    map.transform.rotation = Quaternion.Euler(90, cameraRotation, 0);

        //    transform.rotation = rotation;

        //    cameraLook += 1;
        //    if (cameraLook == 4) cameraLook = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    cameraRotation -= 90.0f;
        //    Quaternion rotation = Quaternion.Euler(45, cameraRotation, 0);

        //    map.transform.rotation = Quaternion.Euler(90, cameraRotation, 0);

        //    transform.rotation = rotation;

        //    cameraLook -= 1;
        //    if (cameraLook == -1) cameraLook = 3;
        //}
    }

    private void LateUpdate()
    {
        //if (cameraLook == 0)
        //{
        //    offsetX = 0;
        //    offsetY = 5;
        //    offsetZ = -5;
        //}

        //else if (cameraLook == 1)
        //{
        //    offsetX = -5;
        //    offsetY = 5;
        //    offsetZ = 0;
        //}

        //else if (cameraLook == 2)
        //{
        //    offsetX = 0;
        //    offsetY = 5;
        //    offsetZ = 5;
        //}

        //else
        //{
        //    offsetX = 5;
        //    offsetY = 5;
        //    offsetZ = 0;
        //}

        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
        transform.LookAt(player.transform);
    }
}
