using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractObject : MonoBehaviour
{
    private Player player;
    private Animator animator;

    public GameObject nearObject;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        nearObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!player.isThrow && nearObject != null)
            {
                if (!player.isInteraction)
                {
                    player.isInteraction = true;
                    animator.SetBool("isInteraction", true);
                }

                //else
                //{
                //    animator.SetBool("isInteraction", false);
                //}
            }
        }
    }

    public void SetInteractionFalse()
    {
        player.isInteraction = false;
    }
}
