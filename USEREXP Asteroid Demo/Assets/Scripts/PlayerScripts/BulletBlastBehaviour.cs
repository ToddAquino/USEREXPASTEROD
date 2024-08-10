using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBlastBehaviour : MonoBehaviour
{
    int delay = 500;
    int currentTime = 0;
    public Transform target;
    GameObject[] existingTargets;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            // Find and assign a random target if not set
            GameObject[] existingTargets = GameObject.FindGameObjectsWithTag("Enemy");
            if (existingTargets.Length > 0)
            {
                target = existingTargets[Random.Range(0, existingTargets.Length)].transform;
            }
        }
        currentTime = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * 5f; // Adjust speed as needed
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (currentTime > 0)
        {
            currentTime--;
        }
        if (currentTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
