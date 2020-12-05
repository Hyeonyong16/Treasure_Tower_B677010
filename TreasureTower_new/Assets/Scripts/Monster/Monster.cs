using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private IState currentState;
    public FieldOfView fieldOfView;

    public NavMeshAgent nav;

    public GameObject weapon;

    public Transform player;
    public Vector3 lastPlayerPos;
    public Vector3 CoinPos;

    public Transform monsterSpawn;
    public Vector3 MonsterSpawnPos;
    public Quaternion monsterFirstRot;     //몬스터가 처음지점에서 바라볼 각도


    //public bool isEnteredPlayer;
    public bool isWatchingPlayer;   //idle, walk일때 플레이어가 몬스터의 시야각에 있는지 체크하는 bool값
    public bool isChasePlayer;
    public bool isEnteredCoin;
    public bool isAnotherSpot;      //기본 위치가 아닌 다른위치인지 확인

    public bool idleCheck = false;  //delayCheck한번만 하기위한 bool값
    public bool isDelayIdle;        //초기 몬스터 위치 외에서 idle 상태일때 5초가 지났는지 체크하는 bool값

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

        isDelayIdle = false;
        //isEnteredPlayer = false;
        isChasePlayer = false;
        isAnotherSpot = false;

        //fieldOfView = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        MonsterSpawnPos = monsterSpawn.position;
        transform.position = MonsterSpawnPos.ignoreY();

        monsterFirstRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        var monsterPos = new Vector3(transform.position.x, 0, transform.position.z);
        dist = Vector3.Distance(playerPos, monsterPos);
        
        /*if (isEnteredPlayer)
        {
            RaycastHit hit;

            //몬스터에서 플레이어 방향 방향백터 구하기
            var heading = player.position - transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;

            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Enemy"));

            if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
                //Debug.Log(hit.transform.name);

                if (hit.transform.name == "Player")
                {
                    isChasePlayer = true; base 
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                        lastPlayerPos = player.position;
                }

                else { isChasePlayer = false; }
            }
        }
        else { isChasePlayer = false; }*/
        

        if (fieldOfView.GetVisibleTargets() == null)
        {
            isWatchingPlayer = false;
        }

        else
        {
            if (fieldOfView.GetVisibleTargets().name == player.name)
            {
                isWatchingPlayer = true;

                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack"))
                    lastPlayerPos = player.position;
            }
        }

        currentState.Update();
    }

    //========================================
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter(this);
    }

    public IState GetCurrentState()
    {
        return currentState;
    }

    //========================================
    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    isEnteredPlayer = true;
        //}

        if (other.tag == "Coin")
        {
            Coin coin = other.GetComponent<Coin>();

            if (coin.isGround && !coin.isChecked)
            {
                coin.isChecked = true;
                isEnteredCoin = true;
                CoinPos = other.gameObject.transform.position;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    Debug.Log("꺼짐");
        //    isEnteredPlayer = false;
        //}
    }

    //========================================
    public void SetisPlayerEnteredFalse()
    {
        weapon.GetComponent<MonsterWeapon>().SetisPlayerEnteredFalse();
    }

    public void CheckIdleTime()
    {
        //if (!(((transform.position.x <= MonsterSpawnPos.x + 1.0f) && (transform.position.x >= MonsterSpawnPos.x - 1.0f))
        //            && ((transform.position.z <= MonsterSpawnPos.z + 1.0f) && (transform.position.z >= MonsterSpawnPos.z - 1.0f))))
        if (!(Vector3.Distance(transform.position.ignoreY(), MonsterSpawnPos.ignoreY()) <= 1))
        {
            if (!idleCheck)
            {
                StartCoroutine("delayCheck");
                idleCheck = true;
            }
        }
    }

    IEnumerator delayCheck()
    {
        yield return new WaitForSeconds(5.0f);
        if (currentState.ToString() == "MonsterIdleState")
        {
            Debug.Log("실행");
            isDelayIdle = true;
        }
    }
}

//Extension method 
//static이 붙어야 함
//파라미터를 this로 받아야함.
public static class vector3Extension
{
    public static Vector3 ignoreY(this Vector3 param)
    {
        return new Vector3(param.x, 0, param.z);
    }
}