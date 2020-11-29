using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowObject : MonoBehaviour
{
    public Transform shotPos;

    public GameObject coin;

    private Player player;
    private Animator animator;

    public float throwPower;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!player.isThrow && !player.isInteraction && player.coinNum > 0)
            {
                player.isThrow = true;
                player.coinNum--;
                animator.SetBool("isThrow", player.isThrow);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Throw") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            player.isThrow = false;
            animator.SetBool("isThrow", player.isThrow);
        }
    }

    public void Throw()
    {
        Vector3 angle;
        angle = transform.forward * 50f;
        angle.y = 25f;

        Debug.Log(transform.forward);
        GameObject instance = Instantiate(coin, shotPos.position, transform.rotation) as GameObject;

        instance.GetComponent<Rigidbody>().AddForce(angle * throwPower, ForceMode.Impulse);
    }
}
