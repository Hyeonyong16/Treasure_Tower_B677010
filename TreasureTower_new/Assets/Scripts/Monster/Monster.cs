using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private IState currentState;

    public NavMeshAgent nav;

    public Transform player;
    public Vector3 lastPlayerPos;

    public bool isEnteredPlayer;
    public bool isChasePlayer;

    public float AttackRange;
    public float dist;          //플레이어와 몬스터 거리

    private Animator animator;

    private void Awake()
    {
        ChangeState(new MonsterIdleState());
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        var monsterPos = new Vector3(transform.position.x, 0, transform.position.z);
        dist = Vector3.Distance(playerPos, monsterPos);

        if (isEnteredPlayer)
        {
            RaycastHit hit;

            //몬스터에서 플레이어 방향 방향백터 구하기
            var heading = player.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            if (Physics.Raycast(transform.position, direction, out hit))
            {
                //Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                //Debug.Log(hit.transform.name);

                if (hit.transform.name == "Player")
                {
                    isChasePlayer = true;
                    if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                        lastPlayerPos = player.position;
                }

                else { isChasePlayer = false; }
            }
        }
        else { isChasePlayer = false; }

        currentState.Update();
    }

    //========================================
    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    //========================================
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnteredPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnteredPlayer = false;
        }
    }

}
