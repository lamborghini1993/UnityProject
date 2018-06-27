using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : Turret {

	public GameObject bulletPrefab; //子弹对象
	public float attchTime = 1; //攻击间隔
	private float runTime = 0;
	// Use this for initialization
	void Start () {
		runTime = attchTime;
	}

	// Update is called once per frame
	void Update () {
		runTime += Time.deltaTime;
		RmoveEmenyNull ();
		if (enemys.Count <= 0) return;
		LookAtEnemy ();
		BulletAttach ();
	}

	void BulletAttach () {
		// 子弹攻击
		if (runTime < attchTime) return;
		runTime = 0;
		GameObject bullet = GameObject.Instantiate (bulletPrefab, bulletPosition.position, bulletPosition.rotation);
		bullet.GetComponent<BulletController> ().SetTarget (enemys[0].transform);
	}

}