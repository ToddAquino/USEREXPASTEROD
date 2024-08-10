using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    int delay = 500;
    int currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = delay;
    }

    // Update is called once per frame
    void Update()
    {
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
