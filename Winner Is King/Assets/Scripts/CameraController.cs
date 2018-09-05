using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController:NetworkBehaviour
{

    public GameObject player;
    Vector3 offset;

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        if(!isLocalPlayer)
            return;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!isLocalPlayer)
            return;
        transform.position = offset + player.transform.position;
        //carmare.position = Vector3.Lerp(carmare.position, pos, carmareSpeed * Time.deltaTime);
    }
}
