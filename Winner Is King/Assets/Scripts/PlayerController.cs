using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour
{

    public float speed = 10;
    float radius, area;
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
        radius = playerSize.x / 2.0f;
        area = Mathf.PI * radius * radius;
        float boundaryX, boundaryY;
        boundaryX = (GlobalVar.Instance.MapX - playerSize.x) / 2.0f;
        boundaryY = (GlobalVar.Instance.MapY - playerSize.y) / 2.0f;
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        //transform.localPosition = new Vector3(boundaryX, -boundaryY, transform.localPosition.z);
    }

    void _Becomebigger(float r)
    {
        float newArea = Mathf.PI * r * r;
        float multiple = newArea / area;
        Debug.Log(string.Format("area:{0} newarea:{1} multiple:{2} localscale:{3}", area, newArea, multiple, transform.localScale));
        transform.localScale += new Vector3(multiple, multiple, multiple);
        area += newArea;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Food"))
        {
            Debug.Log("碰撞");
            float r = collision.gameObject.GetComponent<FoodController>().Radius;
            _Becomebigger(r);
            Destroy(collision.gameObject);
            GenerateFoodManager.Instance.EatFood();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Food"))
        {
            Debug.Log("触发");
            float r = collision.gameObject.GetComponent<FoodController>().Radius;
            _Becomebigger(r);
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
