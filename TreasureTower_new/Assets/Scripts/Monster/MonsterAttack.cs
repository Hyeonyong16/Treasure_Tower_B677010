using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Transform player;

    public Monster monster;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
