using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : IState
{
    private Monster parent;

    private Animator animator;

    public void Enter(Monster parent)
    {
        Debug.Log("AttackState");
        this.parent = parent;

        animator = parent.GetComponent<Animator>();
        animator.SetBool("IsAttacking", true);
        
        parent.nav.isStopped = true;
    }

    public void Exit()
    {
        animator.SetBool("IsAttacking", false);
    }

    public void Update()
    {
        //var heading = parent.player.position - parent.transform.position;
        //var direction = heading.normalized;

        //parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3.0f);

        var heading = parent.player.position.ignoreY() - parent.transform.position.ignoreY();
        var direction = heading.normalized;

        parent.transform.rotation = Quaternion.Lerp(parent.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3.0f);

        if (parent.dist > 2.5f)
        {
            if (parent.isChasePlayer)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    parent.lastPlayerPos = parent.transform.position;
                    parent.ChangeState(new MonsterRunState());
                }
            }

            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.SetBool("ChasePlayer", false);
                    parent.lastPlayerPos = parent.transform.position;
                    parent.ChangeState(new MonsterIdleState());
                }
            }
        }

        else
        {
            
        }
    }
}