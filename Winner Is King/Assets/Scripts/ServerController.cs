using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerController : NetworkBehaviour {

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 100;

    void OnChangeHealth(int curHealth)
    {
        
    }

    [ClientRpc] // 服务端调用客户端里执行,函数名需要Rpc前缀
    void RpcRespawn()
    {
        if(!isLocalPlayer)
            return;
    }

    [Command]   // 客户端调用服务端执行,函数名需要Cmd前缀
    void CmdEatFood()
    {
        if(!isServer)
            return;
        //NetworkServer.Spawn(); // 同步对象到服务端
    }
}
