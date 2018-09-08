using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour
{

    public float initialSpeed = 50;
    public float carmareSpeed = 10;
    [SyncVar(hook = "_ChangeSize")]
    private float speed;
    float radius;
    float area;
    Rigidbody2D rb2d;
    Vector3 offset;

    float Speed
    {
        get
        {
            return initialSpeed - initialSpeed / radius;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLocalPlayer)
            return;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float x = h * Speed * Time.deltaTime + transform.position.x;
        float y = v * Speed * Time.deltaTime + transform.position.y;
        if(x > GlobalVar.Instance.MapX / 2 - radius)
            x = GlobalVar.Instance.MapX / 2 - radius;
        if(x < radius - GlobalVar.Instance.MapX / 2)
            x = radius - GlobalVar.Instance.MapX / 2;
        if(y > GlobalVar.Instance.MapY / 2 - radius)
            y = GlobalVar.Instance.MapY / 2 - radius;
        if(y < radius - GlobalVar.Instance.MapY / 2)
            y = radius - GlobalVar.Instance.MapY / 2;
        transform.position = new Vector3(x, y, 0);
    }

    private void LateUpdate()
    {
        if(!isLocalPlayer)
            return;
        Vector3 pos = transform.position + offset;
        Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, pos, carmareSpeed * Time.deltaTime);
    }

    private void Start()
    {
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        area = Mathf.PI * radius * radius;
    }


    public override void OnStartLocalPlayer()
    {
        // 本地玩家初始化
        offset = Camera.main.gameObject.transform.position - transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        float boundaryX, boundaryY;
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        boundaryX = GlobalVar.Instance.MapX / 2.0f - radius;
        boundaryY = GlobalVar.Instance.MapY / 2.0f - radius;
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        //transform.localPosition = new Vector3(boundaryX, -boundaryY, transform.localPosition.z);
    }

    /// <summary>
    /// 服务端判断碰撞，变大
    /// </summary>
    /// <param name="addR">吃掉半径为addR的球体</param>
    void _Becomebigger(float addR)
    {
        if(!isServer)
            return;
        area += Mathf.PI * addR * addR;
        float newradius = Mathf.Sqrt(area / Mathf.PI);
        float multiple = (newradius - radius) / radius;
        transform.localScale += new Vector3(multiple, multiple, multiple);

        // 纠正半径误差
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        area = Mathf.PI * radius * radius;
    }

    /// <summary>
    /// 服务端改变radius之后 同步到其他客户端
    /// </summary>
    /// <param name="r">将半径变为r</param>
    void _ChangeSize(float r)
    {
        if(isServer)
            return;
        float multiple = (r - radius) / radius;
        transform.localScale += new Vector3(multiple, multiple, multiple);
        area = Mathf.PI * r * r;
        radius = r;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isServer)
            return;
        if(collision.CompareTag("Food"))
        {
            float r = collision.gameObject.GetComponent<FoodController>().Radius;
            GameObject oServerController = GameObject.Find("ServerController");
            oServerController.GetComponent<GenerateFoodManager>().CmdEatFood(collision.gameObject);
            _Becomebigger(r);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isServer)
            return;
        if(collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerStay2D");
            GameObject otherPlayer = collision.gameObject;
            float dis = Vector3.Distance(transform.position, otherPlayer.transform.position);
            if(dis <= radius) // 如果和别人的距离小于等于自己的半径，吃掉别人
            {
                float r = otherPlayer.GetComponent<PlayerController>().radius;
                _Becomebigger(r);
                radius = GetComponent<CircleCollider2D>().radius;
                otherPlayer.GetComponent<PlayerController>().Rpc_Die();
            }
        }
    }

    [ClientRpc]
    public void Rpc_Die()
    {
        transform.localScale = new Vector3(1, 1, 1);
        OnStartLocalPlayer();
    }

}
