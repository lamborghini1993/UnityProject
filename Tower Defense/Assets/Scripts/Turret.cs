using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Use this for initialization
	public List<GameObject> enemys = new List<GameObject> ();
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

	public GameObject bulletPrefab;
	public float attchTime = 1;
	private float runTime = 0;
	public Transform bulletPosition;

	private void Start () {
		runTime = attchTime;
	}
	private void Update () {
		RmoveEmenyNull ();
		runTime += Time.deltaTime;
		if (enemys.Count > 0 && runTime >= attchTime) {
			runTime = 0;
			Attach (); //攻击
		}
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