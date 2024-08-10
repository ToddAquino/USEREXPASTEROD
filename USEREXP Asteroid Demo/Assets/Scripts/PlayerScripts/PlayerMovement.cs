using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Rigidbody2D rb;
    public GameObject sprite;
    Vector2 moveDirection;

    // STUFF THAT ARE COMMENTED OUT IS FOR HAVING THE SHIP STAY IN THE SCREEN AND NOT TELEPORT AT THE EDGES (REMOVE STUFF IN UPDATE AND UNCOMMENT THE REST TO DO SO)
    //Vector2 screenBounds;
    //float objectWidth;
    //float objectHeight;
    // Start is called before the first frame update
    void Start()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //objectWidth = sprite.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        //objectHeight = sprite.transform.GetComponent<SpriteRenderer>().bounds.extents.y;   
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2 (moveX, moveY);
        rb.velocity = new Vector2(moveDirection.x * rotationSpeed, moveDirection.y * speed);
    }

    /*private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }*/
}
