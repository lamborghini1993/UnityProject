using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	// Use this for initialization
	protected List<GameObject> enemys = new List<GameObject> ();

	public GameObject turretHead; //武器头部对象
	
	public Transform bulletPosition; // 子弹位置

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

	protected void LookAtEnemy () {
		Vector3 enemysV = enemys[0].transform.position;
		enemysV.y = transform.position.y;
		turretHead.transform.LookAt (enemysV);
		// turretHead.transform.LookAt (enemys[0].transform);	// y轴也进行lookat
	}

	protected void RmoveEmenyNull () {
		if (enemys.Count <= 0) return;
		if (enemys[0] != null) return; // 第一个为空时才移除
		List<GameObject> index = new List<GameObject> ();
		for (int i = 0; i < enemys.Count; i++) {
			if (enemys[i] == null)
				index.Add (enemys[i]);
		}
		foreach (GameObject obj in index) {
			enemys.Remove (obj);
		}
	}

}