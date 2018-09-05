using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour
{

    public float speed = 10, carmareSpeed = 10;
    float radius, area;
    Rigidbody2D rb2d;
    Vector3 offset;


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

    private void LateUpdate()
    {
        if(!isLocalPlayer)
            return;
        Vector3 pos = transform.position + offset;
        Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, pos, carmareSpeed * Time.deltaTime);

    }

    public override void OnStartLocalPlayer()
    {
        // 本地玩家初始化
        offset = Camera.main.gameObject.transform.position - transform.position;
        rb2d = GetComponent<Rigidbody2D>();

        radius = GetComponent<CircleCollider2D>().radius;
        area = Mathf.PI * radius * radius;
        float boundaryX, boundaryY;
        boundaryX = GlobalVar.Instance.MapX / 2.0f - radius;
        boundaryY = GlobalVar.Instance.MapY / 2.0f - radius;
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        //transform.localPosition = new Vector3(boundaryX, -boundaryY, transform.localPosition.z);
    }

    void _Becomebigger(float r)
    {
        float newArea = Mathf.PI * r * r;
        float multiple = newArea / area;
        //Debug.Log(string.Format("area:{0} newarea:{1} multiple:{2} localscale:{3}", area, newArea, multiple, transform.localScale));
        transform.localScale += new Vector3(multiple, multiple, multiple);
        area += newArea;
        radius = Mathf.Sqrt(area / Mathf.PI);
        NetworkServer.Spawn(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isLocalPlayer)
            return;
        if(collision.CompareTag("Food"))
        {
            float r = collision.gameObject.GetComponent<FoodController>().Radius;
            Cmd_EatFood(collision.gameObject);
            _Becomebigger(r);
        }
    }

    [Command]
    public void Cmd_EatFood(GameObject food)
    {
        if(!isServer)
            return;
        GameObject oServerController = GameObject.Find("ServerController");
        oServerController.GetComponent<GenerateFoodManager>().CmdEatFood(food);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isLocalPlayer)
            return;
        if(collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerStay2D");
            GameObject otherPlayer = collision.gameObject;
            float dis = Vector3.Distance(transform.position, otherPlayer.transform.position);
            if(dis <= radius) // 如果和别人的距离小于等于自己的半径，吃掉别人
            {
                float r = otherPlayer.GetComponent<PlayerController>().radius;
                Cmd_EatFood(otherPlayer);
                _Becomebigger(r);
            }
        }
    }

}
