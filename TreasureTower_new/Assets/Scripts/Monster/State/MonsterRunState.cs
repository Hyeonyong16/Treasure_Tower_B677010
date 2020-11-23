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
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.isChasePlayer)
        {
            animator.SetBool("ChasePlayer", true);
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
                if (((parent.transform.position.x <= parent.lastPlayerPos.x + 0.5f) && (parent.transform.position.x >= parent.lastPlayerPos.x - 0.5f))
                    && ((parent.transform.position.z <= parent.lastPlayerPos.z + 0.5f) && (parent.transform.position.z >= parent.lastPlayerPos.z - 0.5f)))
                {
                    Debug.Log("도착");
                    animator.SetBool("ChasePlayer", false);
                }
            }

            else
            {
                animator.SetBool("ChasePlayer", false);
                parent.nav.isStopped = true;
                parent.ChangeState(new MonsterIdleState());
            }
        }
    }
}
