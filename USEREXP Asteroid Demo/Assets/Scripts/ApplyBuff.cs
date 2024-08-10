using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ApplyBuff : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionInterval = 2f;
    public GameObject powerPopUPprefab;
    public Buff buff;
    Vector2 direction;
    private float screenWidth;
    private float screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomDirection();
        var camera = Camera.main;
        if (camera != null)
        {
            var bounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            screenWidth = bounds.x;
            screenHeight = bounds.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        CheckBounds();
        Destroy(this.gameObject, 10f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Debug.Log(player);
            buff.Apply(player);
            var popUp = Instantiate(powerPopUPprefab, this.transform.position, Quaternion.identity);
            var tmpPopUP = popUp.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            string[] words = this.name.ToString().Split(' ');
            if (words.Length > 0)
            {
                string firstWord = words[0];
                tmpPopUP.text = firstWord + " INCREASED";
            }
            tmpPopUP.text = this.name + " INCREASED";

            Destroy(this.gameObject);
        }
        
    }
    void SetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
    void CheckBounds()
    {
        Vector3 position = transform.position;
        if (Mathf.Abs(position.x) > screenWidth || Mathf.Abs(position.y) > screenHeight)
        {

            direction = -direction;
        }
    }
}
