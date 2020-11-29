using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public GameObject[] CoinBar;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < player.MaxCoinNum; i++)
        {
            if(player.coinNum - 1 >= i)
            {
                CoinBar[i].SetActive(true);
            }

            else
            {
                CoinBar[i].SetActive(false);
            }
        }
    }
}
