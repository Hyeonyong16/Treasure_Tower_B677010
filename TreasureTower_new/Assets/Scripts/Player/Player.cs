using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHP = 1;
    public int HP = 1;

    public int MaxCoinNum = 3;
    public int coinNum = 3;

    public bool isMove = false;
    public bool isCrouch = false;
    public bool isThrow = false;
    public bool isInteraction = false;
    public bool isMakeSomeNoise = false;    //범위 안에서 뛸시 소리나는거 확인하는 변수

    public bool moveFreezeCheck = false;    //잠깐 플레이어의 움직임을 막을 변수

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        coinNum = MaxCoinNum;

        animator = GetComponent<Animator>();
        animator.SetFloat("HP", HP);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("HP", HP);
    }

    public void GetDamaged(int damage)
    {
        HP -= damage;
    }

    public void GetHeal(int heal)
    {
        HP += heal;
    }
}
