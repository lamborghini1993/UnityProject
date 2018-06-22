﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour {

    public TurretData lasser, missile, standard;
    private TurretData selectTurret = null;
    private int money = 1000;
    public Text moneyText = null;
    public Animator moneyAnimator;

    // Use this for initialization
    void Start () { }

    void ChangeMoney (int change = 0) {
        money += change;
        moneyText.text = "￥" + money.ToString ();
    }

    void PressLeftButton () {
        // 判断点击的是否是UI
        if (EventSystem.current.IsPointerOverGameObject ()) {

        } else {
            // 返回一个从相机到屏幕点的光线
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            // debug
            Debug.DrawRay (ray.origin, ray.direction * 10, Color.yellow);
            RaycastHit hit; // 存放碰撞物体的信息
            bool isCollider;

            // ray:射线对象
            // layerMask:选择那个层进行碰撞检测
            // hit:存放碰撞物体的信息
            // maxDistance:最远距离
            isCollider = Physics.Raycast (ray, out hit, 1000, LayerMask.GetMask ("MapCube"));
            if (isCollider) {
                // 得到点击的mapcube的unity游戏对象
                // GameObject mapCube = hit.collider.gameObject;
                // 得到MapCube的脚步对象
                MapCube mapCube = hit.collider.GetComponent<MapCube> ();
                if (mapCube.turretGo == null) {
                    CreateTurret (mapCube);
                } else {
                    // TODO 升级
                }
            }
        }
    }

    void CreateTurret (MapCube mapCube) {
        if (selectTurret == null)
            return;
        if (money < selectTurret.cost) {
            // 钱不够
            moneyAnimator.SetTrigger ("Flicker");
            return;
        }
        ChangeMoney (-selectTurret.cost);
        mapCube.BuildTurret (selectTurret.turretPerfab);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0))
            PressLeftButton ();
    }

    public void OnLasserTurret (bool isOn) {
        if (isOn)
            selectTurret = lasser;
    }

    public void OnMissileTurret (bool isOn) {
        if (isOn)
            selectTurret = missile;
    }

    public void OnStandardTurret (bool isOn) {
        if (isOn)
            selectTurret = standard;
    }
}