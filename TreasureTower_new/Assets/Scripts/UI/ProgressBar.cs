using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject[] progressBar;
    public int progress;

    // Start is called before the first frame update
    void Start()
    {
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < progressBar.Length; i++)
        {
            if(i < progress)
            {
                progressBar[i].SetActive(true);
            }

            else
            {
                progressBar[i].SetActive(false);
            }
        }
    }

    public void initProgress()
    {
        foreach (GameObject gameobj in progressBar)
            gameobj.SetActive(false);
    }
}
