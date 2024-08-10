using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AsteroidBehaviour : MonoBehaviour
{
    public int size = 3;
    [SerializeField] GameObject[] powerUPs;
    Vector2 direction;
    private float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = 0.5f * size * Vector3.one;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);
        Vector2 spawnPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 dirVector = (screenCenter - spawnPosition).normalized;
        direction = dirVector; 

        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

        GameManager.Instance.enemyCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("BulletBlast"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.enemyCount--;
            int scoreMult = Mathf.Clamp(ChainTracker.Instance.currentChain, 0, 10);
            Debug.Log (scoreMult);
            if (collision.CompareTag("Bullet"))
            {
                GameManager.Instance.AddScore(10 * scoreMult);
                ChainTracker.Instance.RegisterKill();
                if (ChainTracker.Instance.currentChain > 0)
                    ChainPopUpGenerator.Instance.CreatePopUp(transform.position, ChainTracker.Instance.currentChain, 10 * scoreMult);
            }
            else
            {
                GameManager.Instance.AddScore(5 * scoreMult);
                ChainTracker.Instance.RegisterKill();
            }

            if (size > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    AsteroidBehaviour newEnemy = Instantiate(this, transform.position, Quaternion.identity);
                    newEnemy.size -= 1;
                }
            }
            this.health = 0;
        }
    }
    //public void SetDirection(Vector2 direction)
    //{
    //    this.direction = direction;
    //}
}
