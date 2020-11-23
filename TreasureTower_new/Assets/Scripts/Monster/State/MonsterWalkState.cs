using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalkState : IState
{
    private Monster parent;

    public void Enter(Monster parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.isChasePlayer)
        {
            parent.ChangeState(new MonsterRunState());
        }
    }
}
