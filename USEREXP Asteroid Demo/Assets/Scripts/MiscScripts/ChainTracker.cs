using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTracker : MonoBehaviour
{
    public static ChainTracker Instance;
    public int totalChain = 0;
    public int currentChain = -1;
    public float timeWindow = 0.5f; // Time duration to count chain kills (in seconds)
    private float lastKillTime;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Update()
    {
        GameManager.Instance.showChain(totalChain);
    }
    public  void RegisterKill()
    {
        if (Time.time - lastKillTime <= timeWindow)
        {
            currentChain++;
            Debug.Log("CHAIN KILL!!! Current Chain: " + currentChain);
        }
        else
        {
            currentChain = 1;
        }
        totalChain += currentChain;
        lastKillTime = Time.time;
    }
}
