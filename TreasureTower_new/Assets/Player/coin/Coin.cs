using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool isGround = false;
    public bool isChecked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGround) StartCoroutine("DestroyThisObject");
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "floor")
            if (!isGround) isGround = true;
    }

    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("삭제");
        Destroy(this.gameObject);
    }
}
