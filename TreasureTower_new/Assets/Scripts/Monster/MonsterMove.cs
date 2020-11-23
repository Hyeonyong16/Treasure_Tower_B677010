using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    public bool isEnteredPlayer;
    public bool isChasePlayer;

    private Transform player;
    private Vector3 lastPlayerPos;

    private NavMeshAgent nav;

    public Monster monster;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnteredPlayer)
        {
            RaycastHit hit;

            //몬스터에서 플레이어 방향 방향백터 구하기
            var heading = player.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            if (Physics.Raycast(transform.position, direction, out hit))
            {
                Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                Debug.Log(hit.transform.name);

                if (hit.transform.name == "Player")
                {
                    isChasePlayer = true;
                    lastPlayerPos = player.position;
                }

                else { isChasePlayer = false; }
            }
        }
        else { isChasePlayer = false; }

        if (isChasePlayer)
        {
            //monster.currentState = Monster.State.Run;
            animator.SetBool("ChasePlayer", true);
            nav.isStopped = false;
            nav.SetDestination(lastPlayerPos);
        }

        else
        {
            if (animator.GetBool("ChasePlayer"))
            {
                if(((transform.position.x <= lastPlayerPos.x+0.5f) && (transform.position.x >= lastPlayerPos.x - 0.5f))
                    && ((transform.position.z <= lastPlayerPos.z + 0.5f) && (transform.position.z >= lastPlayerPos.z - 0.5f)))
                {
                    Debug.Log("도착");
                    animator.SetBool("ChasePlayer", false);
                }
            }

            else
            {
                //monster.currentState = Monster.State.Idle;
                animator.SetBool("ChasePlayer", false);
                nav.isStopped = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            isEnteredPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isEnteredPlayer = false;
        }
    }
}
