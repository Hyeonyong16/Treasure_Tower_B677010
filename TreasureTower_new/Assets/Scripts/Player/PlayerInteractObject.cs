using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractObject : MonoBehaviour
{
    private Player player;
    private Animator animator;

    public GameObject nearObject;

    public GameObject interactionUI;
    public GameObject interactionUI_CoinFull;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();

        interactionUI = GameObject.Find("Canvas").transform.Find("InteractiveUI").transform.
            Find("BackGround_InteractionCheck").gameObject;
        interactionUI_CoinFull = GameObject.Find("Canvas").transform.Find("InteractiveUI").transform.
            Find("BackGround_InteractionCheck_CoinFull").gameObject;

        nearObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(nearObject != null && !player.isInteraction)
        {
            if(nearObject.tag == "CoinsObject" && player.coinNum == player.MaxCoinNum)
            {
                interactionUI.SetActive(false);
                interactionUI_CoinFull.SetActive(true);
            }
            else
            {
                interactionUI.SetActive(true);
                interactionUI_CoinFull.SetActive(false);
            }
        }

        else
        {
            interactionUI.SetActive(false);
            interactionUI_CoinFull.SetActive(false);
        }

        if (player.HP > 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!player.isThrow && nearObject != null && !player.isInteraction)
                {
                    if (nearObject.name == "Coin" && player.coinNum == player.MaxCoinNum)
                    {

                    }

                    else
                    {
                        player.moveFreezeCheck = true;

                        player.isMove = false;
                        player.isInteraction = true;

                        animator.SetBool("isInteraction", true);
                        //animator.SetBool("isMove", false);

                        nearObject.GetComponent<ObjectInteraction>().progressBarUI.transform.parent.gameObject.SetActive(true);
                    }


                    //else
                    //{
                    //    animator.SetBool("isInteraction", false);
                    //}
                }
            }
        }
    }

    public void SetInteractionFalse()
    {
        player.isInteraction = false;
        nearObject.GetComponent<ObjectInteraction>().progressBarUI.initProgress();
        nearObject.GetComponent<ObjectInteraction>().progressBarUI.transform.parent.gameObject.SetActive(false);

        if (nearObject.GetComponent<ObjectInteraction>().maxProgress == nearObject.GetComponent<ObjectInteraction>().progress)
        {
            if(nearObject.tag == "CoinsObject")
            {
                player.coinNum++;
            }

            Destroy(nearObject);
        }
    }

    public void SetMoveFreezeFalse()
    {
        player.moveFreezeCheck = false;
    }
}
