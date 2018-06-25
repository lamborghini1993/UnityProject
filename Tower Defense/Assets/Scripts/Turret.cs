using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Use this for initialization
	private List<GameObject> enemys = new List<GameObject> ();
	private void OnTriggerEnter (Collider other) {
		if (other.tag == "Enemy") {
			enemys.Add (other.gameObject);
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.tag == "Enemy") {
			enemys.Remove (other.gameObject);
		}
	}

	public GameObject bulletPrefab;	//子弹对象
	public GameObject turretHead;	//武器头部对象
	public float attchTime = 1;	//攻击间隔
	private float runTime = 0;
	public Transform bulletPosition;	// 子弹位置

	private void Start () {
		runTime = attchTime;
	}
	private void Update () {
		RmoveEmenyNull ();
		if (enemys.Count <= 0) return;
		runTime += Time.deltaTime;
		LookAtEnemy();
		if (runTime >= attchTime) {
			runTime = 0;
			Attach (); //攻击
		}
	}

	void LookAtEnemy(){
		turretHead.transform.LookAt(enemys[0].transform);
	}

	void RmoveEmenyNull () {
		if (enemys.Count <= 0) return;
		if (enemys[0] != null) return;	// 第一个为空时才移除
		List<GameObject> index = new List<GameObject> ();
		for (int i = 0; i < enemys.Count; i++) {
			if (enemys[i] == null)
				index.Add (enemys[i]);
		}
		foreach (GameObject obj in index) {
			enemys.Remove (obj);
		}
	}

	private void Attach () {
		GameObject bullet = GameObject.Instantiate (bulletPrefab, bulletPosition.position, bulletPosition.rotation);
		bullet.GetComponent<BulletController> ().SetTarget (enemys[0].transform);
	}
}