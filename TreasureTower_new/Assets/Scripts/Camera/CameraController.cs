using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSmoothFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -60;

    public Transform cameraTransform;

    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 camera_offset;

    // Start is called before the first frame update
    void Start()
    {
        camRotation = transform.localRotation;
        camera_offset = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        camRotation.x += Input.GetAxis("Mouse Y") * cameraSmoothFactor * (-1);
        camRotation.y += Input.GetAxis("Mouse X") * cameraSmoothFactor;

        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);

        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        if(Physics.Linecast(transform.position, cameraTransform.position, out hit))
        {
            cameraTransform.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            cameraTransform.localPosition = camera_offset;
        }
    }
}
