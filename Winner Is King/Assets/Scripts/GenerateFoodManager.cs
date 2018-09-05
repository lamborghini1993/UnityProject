using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GenerateFoodManager:NetworkBehaviour
{

    public GameObject food;
    public GameObject background;
    public int maxFood = 50;

    private int curFood = 0;
    private float boundaryWidth, boundaryHeight;

    public override void OnStartServer()
    {
        _InitBackBoundary();
        _InitFood();
    }

    private void _InitBackBoundary()
    {
        Vector3 mapSize = background.GetComponent<SpriteRenderer>().sprite.bounds.size;
        GlobalVar.Instance.MapX = mapSize.x;
        GlobalVar.Instance.MapY = mapSize.y;

        //Vector3 foodSize = food.GetComponent<CircleCollider2D>().bounds.size;
        //boundaryWidth = (mapSize.x - foodSize.x) / 2.0f;
        //boundaryHeight = (mapSize.y - foodSize.y) / 2.0f;

        Vector3 foodSize = food.GetComponent<SpriteRenderer>().sprite.bounds.size; //求的是原来的大小，需要乘以倍数

        float foodWidth, foodHeight;
        foodWidth = foodSize.x * food.transform.localScale.x;
        foodHeight = foodSize.y * food.transform.localScale.y;
        boundaryWidth = (mapSize.x - foodWidth) / 2.0f;
        boundaryHeight = (mapSize.y - foodHeight) / 2.0f;
    }

    private void _InitFood()
    {
        while(curFood < maxFood)
            _GenerateFood();
    }

    Vector3 _RandomCoord()
    {
        float x = Random.Range(-boundaryWidth, boundaryWidth);
        float y = Random.Range(-boundaryHeight, boundaryHeight);
        return new Vector3(x, y, 0);
    }

    private void _GenerateFood()
    {
        if(curFood >= maxFood)
            return;
        GameObject go = GameObject.Instantiate(food, _RandomCoord(), Quaternion.identity);
        NetworkServer.Spawn(go);
        curFood += 1;
    }

    public void EatFood()
    {
        curFood -= 1;
    }
}
