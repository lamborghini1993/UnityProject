using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 地图方块管理

public class MapCube : MonoBehaviour {

    [HideInInspector] // 不需要显示在面板
    public GameObject turretGo;
    public GameObject BuildEffect;

    public void BuildTurret (GameObject turretPerfab) {
        turretGo = GameObject.Instantiate (turretPerfab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(BuildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}