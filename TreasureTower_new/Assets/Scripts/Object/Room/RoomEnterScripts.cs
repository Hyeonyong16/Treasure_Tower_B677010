using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어가 방에 입장하는걸 다룰 스크립트
public class RoomEnterScripts : MonoBehaviour
{
    public Player player;
    private bool isEnterPlayer;     //플레이어가 방에 들어왔는지 체크하는 변수

    public bool roomScriptCheck;    //방에 들어올때 스크립트가 뜰지 말지 결정하는 스크립트
    public GameObject scriptBaseObject; //스크립트를 띄울 오브젝트
    public GameObject scriptObject;     //띄울 스크립트
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnterPlayer)
        {
            scriptObject.SetActive(true);
            scriptObject.transform.position = scriptBaseObject.transform.position;

            //스크립트 UI가 플레이어를 바라보게 하기
            Vector3 tempPlayerPos = new Vector3(player.transform.position.x, scriptBaseObject.transform.position.y, player.transform.position.z);
            //플레이어를 바라보는 방향벡터(y는 같게 해서 위아래로는 안움직이게)
            Vector3 dirToTarget = tempPlayerPos - scriptObject.transform.position;
            //마주보는 형태가 되면 좌우가 반전된 상태가 되기때문에 x, z에 -1을 곱해주어서 바꿈
            dirToTarget.x *= -1;
            dirToTarget.z *= -1;
            
            scriptObject.transform.forward = dirToTarget.normalized;
        }

        else
        {
            scriptObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player.gameObject)
        {
            isEnterPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == player.gameObject)
        {
            isEnterPlayer = false;
        }
    }
}
