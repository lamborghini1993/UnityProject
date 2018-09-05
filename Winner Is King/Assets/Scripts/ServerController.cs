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

    [ClientRpc] // 函数名需要Rpc前缀
    void RpcRespawn()
    {
        if(!isLocalPlayer)
            return;
    }

    [Command]   // 函数名需要Cmd前缀
    void CmdEatFood()
    {
        if(!isServer)
            return;
        //NetworkServer.Spawn(); // 同步对象到服务端
    }
}
