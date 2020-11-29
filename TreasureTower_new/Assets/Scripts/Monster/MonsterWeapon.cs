using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    public Monster monster;
    public bool isPlayerEntered = false;

    private void Start()
    {
        isPlayerEntered = false;
    }

    public void SetisPlayerEnteredFalse()
    {
        isPlayerEntered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (monster.GetCurrentState().ToString() == "MonsterAttackState" && !isPlayerEntered)
            {
                Debug.Log("플레이어 enter");
                other.GetComponent<Player>().GetDamaged(1);
                isPlayerEntered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {

        }
    }
}
