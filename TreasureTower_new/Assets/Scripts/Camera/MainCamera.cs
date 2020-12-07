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

    Renderer objectRenderer;

    private void Start()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;

        player = GameObject.Find("Player");

        cameraRotation = 0;
        cameraLook = 0;
    }

    private void Update()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
        transform.LookAt(player.transform);
    }

    private void LateUpdate()
    {
    }
}

