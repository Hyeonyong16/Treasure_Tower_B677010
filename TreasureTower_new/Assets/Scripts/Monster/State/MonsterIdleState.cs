using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : IState
{
    private Monster parent;

    private Animator animator;

    private float turnSpeed = 5.0f;

    public void Enter(Monster parent)
    {
        Debug.Log("IdleState");
        this.parent = parent;
        animator = parent.GetComponent<Animator>();

        animator.SetBool("IsAttacking", false);
        animator.SetBool("ChasePlayer", false);
        animator.SetBool("isWalking", false);
        
        //parent.nav.isStopped = true;

        parent.isDelayIdle = false;
        parent.idleCheck = false;

        if(!parent.isAnotherSpot)
        {
            parent.fieldOfView.viewAngle = 65;
            parent.fieldOfView.viewRadius = 10;
        }
    }

    public void Exit()
    {
        parent.isDelayIdle = false;
        
    }

    public void Update()
    {
        parent.CheckIdleTime();

        if (!parent.isAnotherSpot)
        {
            parent.gameObject.transform.rotation = Quaternion.Slerp(parent.gameObject.transform.rotation, parent.monsterFirstRot, turnSpeed * Time.deltaTime);
        }

        if (parent.isDelayIdle)
        {
            if (!(Vector3.Distance(parent.transform.position.ignoreY(), parent.MonsterSpawnPos.ignoreY()) <= Mathf.Sqrt(1.0f)))
            {
                parent.isAnotherSpot = false;
                parent.ChangeState(new MonsterWalkState());
            }
        }

        if (parent.player.GetComponent<Player>().HP > 0)
        {
            if (parent.isWatchingPlayer)
            {
                parent.isAnotherSpot = true;
                parent.ChangeState(new MonsterRunState());
            }

            else
            {
                if (parent.isHearSound)
                {
                    parent.isAnotherSpot = true;
                    parent.ChangeState(new MonsterWalkState());
                }

                if (parent.isEnteredCoin)
                {
                    parent.isAnotherSpot = true;
                    parent.ChangeState(new MonsterWalkState());
                }
            }
        }
    }

}
