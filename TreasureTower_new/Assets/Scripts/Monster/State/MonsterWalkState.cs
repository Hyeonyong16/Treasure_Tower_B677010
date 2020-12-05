using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalkState : IState
{
    private Monster parent;

    private Animator animator;

    public void Enter(Monster parent)
    {
        Debug.Log("WalkState");
        this.parent = parent;
        animator = parent.GetComponent<Animator>();

        parent.nav.isStopped = false;
        animator.SetBool("isWalking", true);

        if (!parent.isAnotherSpot)
        {
            parent.fieldOfView.viewAngle = 65;
            parent.fieldOfView.viewRadius = 10;
        }

        else
        {
            parent.fieldOfView.viewAngle = 360;
            parent.fieldOfView.viewRadius = 12;
        }
    }

    public void Exit()
    {
        animator.SetBool("isWalking", false);
        parent.isEnteredCoin = false;
    }

    public void Update()
    {
        if (!parent.isWatchingPlayer)
        {
            if (parent.isEnteredCoin)
            {
                parent.nav.SetDestination(parent.CoinPos);
                
                //if (((parent.transform.position.x <= parent.CoinPos.x + 1.0f) && (parent.transform.position.x >= parent.CoinPos.x - 1.0f))
                //            && ((parent.transform.position.z <= parent.CoinPos.z + 1.0f) && (parent.transform.position.z >= parent.CoinPos.z - 1.0f)))
                if (Vector3.Distance(parent.transform.position.ignoreY(), parent.CoinPos.ignoreY()) <= 1)
                {
                    parent.nav.isStopped = true;
                    parent.ChangeState(new MonsterIdleState());
                }
            }

            else
            {
                parent.nav.SetDestination(parent.MonsterSpawnPos);
                
                //if (((parent.transform.position.x <= parent.MonsterSpawnPos.x + 1.0f) && (parent.transform.position.x >= parent.MonsterSpawnPos.x - 1.0f))
                //            && ((parent.transform.position.z <= parent.MonsterSpawnPos.z + 1.0f) && (parent.transform.position.z >= parent.MonsterSpawnPos.z - 1.0f)))
                if (Vector3.Distance(parent.transform.position.ignoreY(), parent.MonsterSpawnPos.ignoreY()) <= 1)
                {
                    parent.nav.isStopped = true;
                    parent.isAnotherSpot = false;
                    parent.ChangeState(new MonsterIdleState());
                }
            }
        }

        else
        {
            parent.ChangeState(new MonsterRunState());
        }
    }
}
