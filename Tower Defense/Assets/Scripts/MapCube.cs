using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// 地图方块管理

public class MapCube : MonoBehaviour {

    [HideInInspector] // 不需要显示在面板
    public GameObject turretGo; //炮塔
    public GameObject BuildEffect; //建造时的特效
    private Color oldColor;
    private Renderer cubeRenderer;

    public void BuildTurret (GameObject turretPerfab) {
        turretGo = GameObject.Instantiate (turretPerfab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate (BuildEffect, transform.position, Quaternion.identity);
        cubeRenderer.material.color = oldColor;
        Destroy (effect, 1);
    }

    // Use this for initialization
    void Start () {
        cubeRenderer = GetComponent<Renderer> ();
        oldColor = cubeRenderer.material.color;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnMouseEnter () {
        if (turretGo != null || EventSystem.current.IsPointerOverGameObject ()) return; //有炮塔或者点击为ui直接return
        cubeRenderer.material.color = Color.red;
    }

    private void OnMouseExit () {
        cubeRenderer.material.color = oldColor;
    }

}