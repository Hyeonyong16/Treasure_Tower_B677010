using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalkState : IState
{
    private Monster parent;

    private Animator animator;

    private bool ChaseSound;

    public void Enter(Monster parent)
    {
        ChaseSound = false;

        Debug.Log("WalkState");
        this.parent = parent;
        animator = parent.GetComponent<Animator>();

        parent.nav.isStopped = false;
        animator.SetBool("isWalking", true);
    }

    public void Exit()
    {
        animator.SetBool("isWalking", false);
        parent.isEnteredCoin = false;
        parent.isHearSound = false;
    }

    public void Update()
    {
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

        if (parent.player.gameObject.GetComponent<Player>().isMakeSomeNoise != true || parent.dist > parent.hearDistance)
        {
            parent.isHearSound = false;
        }

        if (parent.player.GetComponent<Player>().HP > 0)
        {
            if (!parent.isWatchingPlayer)
            {
                if (parent.isHearSound || ChaseSound)
                {
                    parent.isAnotherSpot = true;
                    parent.nav.SetDestination(parent.SoundPos);
                    ChaseSound = true;
                    if (Vector3.Distance(parent.transform.position.ignoreY(), parent.SoundPos.ignoreY()) <= 1)
                    {
                        parent.nav.isStopped = true;
                        parent.ChangeState(new MonsterIdleState());
                    }

                }

                else if (parent.isEnteredCoin && !ChaseSound)
                {
                    parent.isAnotherSpot = true;
                    parent.nav.SetDestination(parent.CoinPos);
                    if (Vector3.Distance(parent.transform.position.ignoreY(), parent.CoinPos.ignoreY()) <= 1)
                    {
                        parent.nav.isStopped = true;
                        parent.ChangeState(new MonsterIdleState());
                    }
                }


                else
                {
                    parent.nav.SetDestination(parent.MonsterSpawnPos);
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

        else
        {
            parent.nav.SetDestination(parent.MonsterSpawnPos);
            if (Vector3.Distance(parent.transform.position.ignoreY(), parent.MonsterSpawnPos.ignoreY()) <= 1)
            {
                parent.nav.isStopped = true;
                parent.isAnotherSpot = false;
                parent.ChangeState(new MonsterIdleState());
            }
        }
    }
}
