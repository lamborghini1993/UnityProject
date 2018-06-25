using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// 地图方块管理

public class MapCube : MonoBehaviour {

    [HideInInspector] // 不需要显示在面板
    public GameObject turretGo; //炮塔
    [HideInInspector]
    public bool isUpgrade = false;
    public GameObject BuildEffect; //建造时的特效
    private Color oldColor;
    private Renderer cubeRenderer; // cube的render，修改颜色用途
    private TurretData turretData; //保存建造的turretData（获取未升级、升级）

    public void BuildTurret (TurretData turretData) {
        this.turretData = turretData;
        isUpgrade = false;
        turretGo = GameObject.Instantiate (turretData.turretPerfab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate (BuildEffect, transform.position, Quaternion.identity);
        cubeRenderer.material.color = oldColor;
        Destroy (effect, 1);
    }

    public void TurretUpgrade () {
        if (isUpgrade) return;
        isUpgrade = true;
        Destroy (turretGo);
        turretGo = GameObject.Instantiate (turretData.turrentUpgradePerfab, transform.position, Quaternion.identity);
    }

    public void TurretDestory () {
        isUpgrade = false;
        Destroy (turretGo);
        turretData = null;
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