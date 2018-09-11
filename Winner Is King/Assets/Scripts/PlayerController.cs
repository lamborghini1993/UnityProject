using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController:NetworkBehaviour
{

    public float initialSpeed = 50;
    public float carmareSpeed = 10;

    [SyncVar(hook = "_ChangeSize")]
    float radius;
    private float speed;
    float area;
    Vector3 offset;
    float mapX = 0, mapY = 0;
    float boundaryX, boundaryY;
    float cameraSize;

    float Speed
    {
        get
        {
            return initialSpeed - initialSpeed / radius;
        }
    }

    float InitialRadius
    {
        get
        {
            return GetComponent<CircleCollider2D>().radius;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isLocalPlayer)
            return;
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        if(v == 0 && h == 0)
            return;
        if(mapX == 0 && mapY == 0)
            return;
        float x = h * Speed * Time.deltaTime + transform.position.x;
        float y = v * Speed * Time.deltaTime + transform.position.y;
        if(x > mapX / 2 - radius)
            x = mapX / 2 - radius;
        if(x < radius - mapX / 2)
            x = radius - mapX / 2;
        if(y > mapY / 2 - radius)
            y = mapY / 2 - radius;
        if(y < radius - mapY / 2)
            y = radius - mapY / 2;
        transform.position = new Vector3(x, y, 0);
    }

    [Command]
    void Cmd_GetMapBoundary()
    {
        if(!isServer)
            return;
        Rpc_SetMapBoundary(GlobalVar.Instance.MapX, GlobalVar.Instance.MapY);
    }

    [ClientRpc]
    void Rpc_SetMapBoundary(float x, float y)
    {
        if(!isLocalPlayer)
            return;
        //Debug.Log("Rpc_SetMapBoundary");
        mapX = x;
        mapY = y;
        boundaryX = x / 2.0f - InitialRadius;
        boundaryY = y / 2.0f - InitialRadius;
        Init();
    }

    private void LateUpdate()
    {
        if(!isLocalPlayer)
            return;
        Vector3 pos = transform.position;
        pos.z = Camera.main.gameObject.transform.position.z;
        Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, pos, carmareSpeed * Time.deltaTime);
    }

    public void Start()
    {
        Init();
    }

    private void Init()
    {
        radius = InitialRadius;
        area = Mathf.PI * radius * radius;
        transform.localScale = new Vector3(1, 1, 1);
        if(isLocalPlayer)
        {
            float x = Random.Range(-boundaryX, boundaryX);
            float y = Random.Range(-boundaryY, boundaryY);
            Debug.Log(string.Format("Boundary x:{0} y:{1} Random x:{2} y:{3}", boundaryX, boundaryY, x, y));
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }
    }


    public override void OnStartLocalPlayer()
    {
        //Debug.Log("OnStartLocalPlayer");
        // 本地玩家初始化
        Cmd_GetMapBoundary();
        cameraSize = Camera.main.orthographicSize;
        //radius = InitialRadius * transform.localScale.x;
        //boundaryX = GlobalVar.Instance.MapX / 2.0f - radius;
        //boundaryY = GlobalVar.Instance.MapY / 2.0f - radius;
        //Debug.Log(string.Format("Boundary x:{0} y:{1} Random x:{2} y:{3}", boundaryX, boundaryY, 0, 0));
        //Init();
        //float x = Random.Range(-boundaryX, boundaryX);
        //float y = Random.Range(-boundaryY, boundaryY);
        ////Debug.Log(string.Format("Boundary x:{0} y:{1} Random x:{2} y:{3}", boundaryX, boundaryY, x, y));
        //transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        ////transform.localPosition = new Vector3(boundaryX, -boundaryY, transform.localPosition.z);
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
        float oldRadius = radius;
        radius = Mathf.Sqrt(area / Mathf.PI);
        float multiple = (radius - oldRadius) / InitialRadius;
        transform.localScale += new Vector3(multiple, multiple, multiple);
        Debug.Log(string.Format("Old:{0} Eat:{1} now:{2}", oldRadius, addR, radius));
        //Rpc_ChangeCameraSize(Camera.main.orthographicSize * radius / oldRadius);
    }


    /// <summary>
    /// 服务端改变radius之后 同步到其他客户端
    /// </summary>
    /// <param name="r">将半径变为r</param>
    void _ChangeSize(float r)
    {
        if(isServer)
            return;
        Debug.Log(string.Format("old:{0} new:{1}", radius, r));
        float multiple = (r - radius) / InitialRadius;
        transform.localScale += new Vector3(multiple, multiple, multiple);
        area = Mathf.PI * r * r;
        radius = r;
        if(isLocalPlayer)
        {
            float size = radius * cameraSize / InitialRadius;
            Camera.main.orthographicSize = size;
        }
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
            GameObject otherPlayer = collision.gameObject;
            float dis = Vector3.Distance(transform.position, otherPlayer.transform.position);
            if(dis <= radius) // 如果和别人的距离小于等于自己的半径，吃掉别人
            {
                float r = otherPlayer.GetComponent<PlayerController>().radius;
                _Becomebigger(r);
                otherPlayer.GetComponent<PlayerController>().Init();
                otherPlayer.GetComponent<PlayerController>().Rpc_Die();
            }
        }
    }

    [ClientRpc]
    public void Rpc_Die()
    {
        if(!isLocalPlayer)
            return;
        Init();
        //Destroy(this.gameObject);
        //Network.Disconnect();
        //OnPlayerDisconnected(this.gameObject as NetworkPlayer);
        //OnPlayerDisconnected();
        //radius = InitialRadius;
        //transform.localScale = new Vector3(1, 1, 1);
        //OnStartLocalPlayer();
    }

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

}
