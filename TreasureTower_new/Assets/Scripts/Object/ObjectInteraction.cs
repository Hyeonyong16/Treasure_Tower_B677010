using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public bool isPlayerEnter;
    public Player player;

    public int progress = 0;

    public int maxProgress;

    private bool checkCoroutine = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerEnter = false;
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(!checkCoroutine && player.isInteraction)
            {
                if(progress < maxProgress)
                    StartCoroutine("interact");
            }

            if (maxProgress == progress)
            {
                if (player.isInteraction)
                {
                    player.GetComponent<Animator>().SetBool("isInteraction", false);

                    //======================
                    Destroy(this.gameObject);
                    //======================
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isPlayerEnter = true;
            player = other.GetComponent<Player>();
            player.GetComponent<PlayerInteractObject>().nearObject = this.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            isPlayerEnter = false;
            player.GetComponent<PlayerInteractObject>().nearObject = null;
            player = null;
        }
    }

    IEnumerator interact()
    {
        checkCoroutine = true;
        yield return new WaitForSeconds(2.0f);
        progress++;
        checkCoroutine = false;
    }
}
