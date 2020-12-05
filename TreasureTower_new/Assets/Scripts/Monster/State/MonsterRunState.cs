using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRunState : IState
{
    private Monster parent;

    private Animator animator;

    public void Enter(Monster parent)
    {
        Debug.Log("RunState");
        this.parent = parent;
        animator = parent.GetComponent<Animator>();

        parent.nav.isStopped = false;

        parent.isChasePlayer = true;
        
        animator.SetBool("ChasePlayer", true);

        parent.fieldOfView.viewAngle = 360;
        parent.fieldOfView.viewRadius = 14.5f;
    }

    public void Exit()
    {
        parent.isEnteredCoin = false;
    }

    public void Update()
    {
        if (parent.isWatchingPlayer)
        {
            parent.nav.SetDestination(parent.lastPlayerPos);
            
            if(parent.dist <= 2.5f)
            {
                parent.ChangeState(new MonsterAttackState());
            }
        }

        else
        {
            if (animator.GetBool("ChasePlayer"))
            {
                if ((Vector3.Distance(parent.transform.position.ignoreY(), parent.lastPlayerPos.ignoreY()) <= Mathf.Sqrt(0.5f)))
                {
                    Debug.Log("도착");
                    animator.SetBool("ChasePlayer", false);
                }
            }

            else
            {
                animator.SetBool("ChasePlayer", false);
                parent.nav.isStopped = true;
                parent.isChasePlayer = false;
                parent.ChangeState(new MonsterIdleState());
            }
        }
    }
}
