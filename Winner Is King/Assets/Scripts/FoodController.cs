using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FoodController:NetworkBehaviour
{

    public Color[] foodColor;
    private float radius, area;
    private int index;

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

    //public override void OnStartServer()
    //{
    //    Vector3 foodSize = GetComponent<CircleCollider2D>().bounds.size;
    //    Radius = foodSize.x / 2.0f;
    //    Area = Mathf.PI * Radius * Radius;
    //    index = Random.Range(0, foodColor.Length);
    //    GetComponent<SpriteRenderer>().color = foodColor[index];
    //}

    public void Start()
    {
        Vector3 foodSize = GetComponent<CircleCollider2D>().bounds.size;
        Radius = foodSize.x / 2.0f;
        Area = Mathf.PI * Radius * Radius;
        index = Random.Range(0, foodColor.Length);
        GetComponent<SpriteRenderer>().color = foodColor[index];
    }

}
