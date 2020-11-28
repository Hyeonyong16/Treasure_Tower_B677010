using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : IState
{
    private Monster parent;

    private Animator animator;

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
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        parent.CheckIdleTime();

        if (parent.isDelayIdle)
        {
            if (!(((parent.transform.position.x <= parent.MonsterSpawnPos.x + 1.0f) && (parent.transform.position.x >= parent.MonsterSpawnPos.x - 1.0f))
                        && ((parent.transform.position.z <= parent.MonsterSpawnPos.z + 1.0f) && (parent.transform.position.z >= parent.MonsterSpawnPos.z - 1.0f))))
            {
                parent.isDelayIdle = false;
                parent.ChangeState(new MonsterWalkState());
            }
        }

        if(parent.isChasePlayer)
        {
            parent.isDelayIdle = false;
            parent.ChangeState(new MonsterRunState());
        }

        else
        {
            if(parent.isEnteredCoin)
            {
                parent.ChangeState(new MonsterWalkState());
            }
        }
    }

}
