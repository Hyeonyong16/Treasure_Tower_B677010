using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOutline : MonoBehaviour
{
    public GameObject changeObject;
    public Material outlineMaterial;
    public Material baseMaterial;

    MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = changeObject.GetComponent<MeshRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            meshRenderer.material = outlineMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            meshRenderer.material = baseMaterial;
        }
    }
}
