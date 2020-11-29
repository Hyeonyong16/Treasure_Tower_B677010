﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHP = 4;
    public int HP = 4;

    public int MaxCoinNum = 3;
    public int coinNum = 3;

    public bool isMove = false;
    public bool isCrouch = false;
    public bool isThrow = false;
    public bool isInteraction = false;

    public bool moveFreezeCheck = false; //잠깐 플레이어의 움직임을 막을 변수

    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        coinNum = MaxCoinNum;
    }

    // Update is called once per frame
    void Update()
    {
        
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
