using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 发射激光的炮塔
public class LaserTurret : Turret {

	public LineRenderer laserLineRenderer;
	public float damage = 70; // 每秒伤害
	void Update () {
		RmoveEmenyNull ();
		if (enemys.Count <= 0) {
			laserLineRenderer.enabled = false;
			return;
		}
		LookAtEnemy ();
		LaserAttch ();
	}

	void LaserAttch () {
		// 激光攻击
		laserLineRenderer.enabled = true;
		laserLineRenderer.SetPositions (new Vector3[] { bulletPosition.position, enemys[0].transform.position });
		// bullet.GetComponent<BulletController> ().LaserDamage ();
		enemys[0].transform.GetComponent<EnemyMove> ().TakeDamage (damage * Time.deltaTime);
	}
}