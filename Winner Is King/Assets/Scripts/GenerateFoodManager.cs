using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFoodManager:MonoBehaviour
{

    private int curFood = 0;
    public int maxFood = 30;
    public GameObject food;
    public GameObject background;
    private float boundaryWidth, boundaryHeight;
    private float foodWidth, foodHeight;
    private static GenerateFoodManager m_instance;

    private GenerateFoodManager() { }
    public static GenerateFoodManager Instance
    {
        get
        {
            if(m_instance == null)
                m_instance = new GenerateFoodManager();
            return m_instance;
        }

    }

    private void Start()
    {
        Vector3 mapSize = background.GetComponent<SpriteRenderer>().sprite.bounds.size;
        GlobalVar.Instance.MapX = mapSize.x;
        GlobalVar.Instance.MapY = mapSize.y;

        //Vector3 foodSize = food.GetComponent<CircleCollider2D>().bounds.size;
        //boundaryWidth = (mapSize.x - foodSize.x) / 2.0f;
        //boundaryHeight = (mapSize.y - foodSize.y) / 2.0f;

        Vector3 foodSize = food.GetComponent<SpriteRenderer>().sprite.bounds.size; //求的是原来的大小，需要乘以倍数
        foodWidth = foodSize.x * food.transform.localScale.x;
        foodHeight = foodSize.y * food.transform.localScale.y;
        boundaryWidth = (mapSize.x - foodWidth) / 2.0f;
        boundaryHeight = (mapSize.y - foodHeight) / 2.0f;
        Debug.Log(mapSize);
        Debug.Log(foodSize);
        //GameObject.Instantiate(food, new Vector3(boundaryWidth, boundaryHeight, 0), Quaternion.identity);
    }

    Vector3 RandomCoord()
    {
        float x = Random.Range(-boundaryWidth, boundaryWidth);
        float y = Random.Range(-boundaryHeight, boundaryHeight);
        return new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(curFood >= maxFood)
            return;
        GameObject go = GameObject.Instantiate(food, RandomCoord(), Quaternion.identity);
        go.GetComponent<FoodController>().Init();
        curFood += 1;
    }

    public void EatFood()
    {
        curFood -= 1;
    }
}
