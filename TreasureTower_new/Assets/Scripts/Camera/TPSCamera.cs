using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;
    public float dist = 4f;

    public float xSpeed = 220.0f;
    public float ySpeed = 100.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    public float yRotMin = -60.0f;
    public float yRotMax = 60.0f;
    
    private Vector3 targetPos;

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;

        x = angles.x;
        y = angles.y;
    }

    private void Update()
    {
        //카메라 회전속도 계산
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        //앵글값 정하기(y값 제한)
        y = ClampAngle(y, yRotMin, yRotMax);
        
        //카메라 위치 변화 계산
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 1.0f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);
        
        transform.rotation = rotation;
        transform.position = position;
        
    }

    private void LateUpdate()
    {

    }

}
