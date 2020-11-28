using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedObjectAlphaChange : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;

    Renderer objectRenderer;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        var heading = player.transform.position - mainCamera.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));

        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(mainCamera.transform.position, direction * hit.distance, Color.red);
            //Debug.Log("Distance: " + distance);
            Debug.Log(hit.transform.name);

            if (hit.transform.tag == "Object")
            {
                if (objectRenderer != hit.transform.GetComponent<Renderer>())
                {
                    if (objectRenderer != null)
                    {
                        Debug.Log("알파값 복구");
                        Material mat = objectRenderer.material;
                        Color matColor = mat.color;
                        matColor.a = 1.0f;
                        mat.color = matColor;

                        objectRenderer = null;
                    }

                    objectRenderer = hit.transform.GetComponent<Renderer>();

                    if (objectRenderer != null)
                    {
                        Material mat = objectRenderer.material;
                        Color matColor = mat.color;
                        matColor.a = 0.5f;
                        mat.color = matColor;
                    }
                }
            }

            else
            {
                if (objectRenderer != null)
                {
                    Debug.Log("알파값 복구");
                    Material mat = objectRenderer.material;
                    Color matColor = mat.color;
                    matColor.a = 1.0f;
                    mat.color = matColor;

                    objectRenderer = null;
                }
            }
        }
    }
}
