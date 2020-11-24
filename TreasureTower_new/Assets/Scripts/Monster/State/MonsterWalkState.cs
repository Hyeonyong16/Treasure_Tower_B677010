﻿using System.Collections;
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
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (!parent.isChasePlayer)
        {
            parent.nav.SetDestination(parent.MonsterSpawnPos);

            if (((parent.transform.position.x <= parent.MonsterSpawnPos.x + 1.0f) && (parent.transform.position.x >= parent.MonsterSpawnPos.x - 1.0f))
                        && ((parent.transform.position.z <= parent.MonsterSpawnPos.z + 1.0f) && (parent.transform.position.z >= parent.MonsterSpawnPos.z - 1.0f)))
            {
                animator.SetBool("isWalking", false);
                parent.nav.isStopped = true;
                parent.ChangeState(new MonsterIdleState());
            }
        }

        else
        {
            parent.ChangeState(new MonsterRunState());
        }
    }
}
