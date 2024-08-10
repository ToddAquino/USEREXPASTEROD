using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOPBehaviour : MonoBehaviour
{
    public float speed;

    private float waitTime;
    public float startWait;

    public GameObject[] movePoint;
    private int randomPoint;
    [SerializeField] GameObject[] powerUPs;
    // Start is called before the first frame update
    void Start()
    {
        movePoint = GameObject.FindGameObjectsWithTag("Waypoints");
        waitTime = startWait;
        randomPoint = Random.Range(0, movePoint.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoint[randomPoint].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoint[randomPoint].transform.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomPoint = Random.Range(0, movePoint.Length);
                waitTime = startWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.enemyCount--;
            int scoreMult = Mathf.Clamp(ChainTracker.Instance.currentChain, 0, 10);
            Debug.Log(scoreMult);
            GameManager.Instance.AddScore(10 * scoreMult);
            ChainTracker.Instance.RegisterKill();
            if (ChainTracker.Instance.currentChain > 0)
                ChainPopUpGenerator.Instance.CreatePopUp(transform.position, ChainTracker.Instance.currentChain, 10 * scoreMult);

            float randomV = Random.Range(0f, 100f);
            if (randomV < 20f * ChainTracker.Instance.currentChain / 2 * 0.1)
            {
                GameObject spawnBuff = Instantiate(powerUPs[Random.Range(0, powerUPs.Length)], transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

}
