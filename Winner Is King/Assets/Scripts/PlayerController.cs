using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour
{

    public float speed = 10;
    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLocalPlayer)
            return;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(new Vector2(h, v) * speed * Time.deltaTime);
        //transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }

    public override void OnStartLocalPlayer()
    {
        // 本地玩家初始化
        //Vector3 mapSize = background.GetComponent<SpriteRenderer>().sprite.bounds.size;
        Vector3 playerSize = GetComponent<CircleCollider2D>().bounds.size;
        Debug.Log(playerSize);
        float boundaryX, boundaryY;
        boundaryX = (GlobalVar.Instance.MapX - playerSize.x) / 2.0f;
        boundaryY = (GlobalVar.Instance.MapY - playerSize.y) / 2.0f;
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        //transform.localPosition = new Vector3(boundaryX, -boundaryY, transform.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Food"))
        {
            Debug.Log("---Eat Food---");
            Destroy(collision.gameObject);
            GenerateFoodManager.Instance.EatFood();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("---Stay---");
            Debug.Log(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("---Exit---");
        Debug.Log(collision);
    }
}
