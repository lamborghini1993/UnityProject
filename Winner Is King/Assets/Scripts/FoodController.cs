using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FoodController:NetworkBehaviour
{

    public Color[] foodColor;
    private float radius, area;
    private int colorIndex;

    public float Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
        }
    }

    public float Area
    {
        get
        {
            return area;
        }

        set
        {
            area = value;
        }
    }

    public override void OnStartServer()
    {
        colorIndex = Random.Range(0, foodColor.Length);
        Vector3 foodSize = GetComponent<CircleCollider2D>().bounds.size;
        Radius = foodSize.x / 2.0f;
        Area = Mathf.PI * Radius * Radius;
        GetComponent<SpriteRenderer>().color = foodColor[colorIndex];
    }

    /// <summary>
    /// 向服务端获取颜色下标
    /// </summary>
    /// <returns></returns>
    //[Command]
    //public int Cmd_GetColorIndex()
    //{
    //    return colorIndex;
    //}

    //private void Start()
    //{
    //    if(isServer)
    //        GetComponent<SpriteRenderer>().color = foodColor[colorIndex];
    //    else
    //        GetComponent<SpriteRenderer>().color = foodColor[Cmd_GetColorIndex()];
    //}
}
