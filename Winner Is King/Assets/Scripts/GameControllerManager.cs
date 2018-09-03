using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerManager : MonoBehaviour {

    public int maxFood = 30;
    public GameObject food;
    public GameObject background;

    private float boundaryWidth, boundaryHeight;
    private float foodWidth, foodHeight;
    // Use this for initialization
    void Start () {
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

        GenerateFoodManager.Instance.MaxFood = maxFood;
        GenerateFoodManager.Instance.boundaryHeight = boundaryHeight;
        GenerateFoodManager.Instance.boundaryWidth = boundaryWidth;
    }
	
	// Update is called once per frame
	void Update () {
        GenerateFoodManager.Instance.GenerateFood(food);

    }
}
