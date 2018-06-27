using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	private Transform targetEnemy;
	public float damage = 30;
	public GameObject explosionEffectPrefab;
	public float speed = 70;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (targetEnemy == null) {
			Destroy (this.gameObject);
			return;
		}
		transform.LookAt (targetEnemy.position);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	public void SetTarget (Transform transform) {
		targetEnemy = transform;
	}

	private void OnTriggerEnter (Collider other) {
		if (other.tag != "Enemy")
			return;
		other.GetComponent<EnemyMove> ().TakeDamage (damage); //伤害
		GameObject attchEffect = GameObject.Instantiate (explosionEffectPrefab, transform.position, transform.rotation); //爆炸特效
		Destroy (attchEffect, 1);
		Destroy (this.gameObject); //摧毁子弹游戏物体
	}

}