using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildController : MonoBehaviour {

    public TurretData lasser, missile, standard;
    private TurretData selectTurret = null; // ui上选择的炮塔类型
    private MapCube selectMapcube; // 保存上次选择的炮塔
    private int money = 1000;
    public Text moneyText = null;
    public Animator moneyAnimator;
    public GameObject canvasTurretUpgrade;
    public Button canvasUpgradeButton;

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
                    if (selectMapcube == mapCube && canvasTurretUpgrade.activeInHierarchy) {
                        HideTurretUpgradeCanvas ();
                    } else {
                        ShowTurretUpgradeCanvas (mapCube.turretGo.transform.position, mapCube.isUpgrade);
                    }
                    selectMapcube = mapCube;
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
        mapCube.BuildTurret (selectTurret);
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

    void ShowTurretUpgradeCanvas (Vector3 position, bool isUpgrade) {
        // Debug.Log (isUpgrade);   // TODO bug 升级之后的还是会闪烁点击升级
        canvasUpgradeButton.interactable = !isUpgrade;
        canvasTurretUpgrade.SetActive (true);
        canvasTurretUpgrade.transform.position = position;
    }

    void HideTurretUpgradeCanvas () {
        canvasTurretUpgrade.SetActive (false);
        canvasUpgradeButton.interactable = true;
    }

    public void OnCanvasUpgradeButtonDown () {
        selectMapcube.TurretUpgrade ();
        HideTurretUpgradeCanvas ();
    }

    public void OnCanvasDestroyButtonDown () {
        selectMapcube.TurretDestory ();
        HideTurretUpgradeCanvas ();
    }
}