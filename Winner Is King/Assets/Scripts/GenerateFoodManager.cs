using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFoodManager
{
    private int curFood = 0;
    private int maxFood;
    private static GenerateFoodManager m_instance;
    public float boundaryWidth, boundaryHeight;

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

    public int MaxFood
    {
        get
        {
            return maxFood;
        }

        set
        {
            maxFood = value;
        }
    }

    Vector3 RandomCoord()
    {
        float x = Random.Range(-boundaryWidth, boundaryWidth);
        float y = Random.Range(-boundaryHeight, boundaryHeight);
        return new Vector3(x, y, 0);
    }

    // Update is called once per frame
    public void GenerateFood(GameObject food)
    {
        if(curFood >= MaxFood)
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
