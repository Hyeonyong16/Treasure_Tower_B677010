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
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if(parent.isChasePlayer)
        {
            parent.ChangeState(new MonsterRunState());
        }
    }
}
