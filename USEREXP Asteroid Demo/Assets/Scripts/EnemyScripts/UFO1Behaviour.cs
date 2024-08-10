using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO1Behaviour : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private Transform target;
    private float timer;
    public float speed;
    [SerializeField] GameObject[] powerUPs;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position*0.15f, speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer > 3)
        {
            timer = 0;
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
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

