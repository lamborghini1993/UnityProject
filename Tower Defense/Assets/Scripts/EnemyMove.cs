using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public float speed = 1;
    private Transform[] positons;
    public GameObject explosionEffect;
    public int HP = 100;
    private int index = 0;
    // Use this for initialization
    void Start () {
        positons = WayPoints.positons;
    }

    // Update is called once per frame
    void Update () {
        PositonMove ();
    }

    void PositonMove () {
        if (index >= positons.Length) {
            return;
        }
        transform.Translate ((positons[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance (positons[index].position, transform.position) < 0.2f) {
            index++;
        }
        if (index >= positons.Length) {
            MyDestory ();
        }
    }

    void MyDestory () {
        Destroy (this.gameObject);
    }

    private void OnDestroy () {
        GameCrontroller.EnemyDisappear ();
    }

    public void TakeDamage (int damage) {
        HP -= damage;
        if (HP <= 0) {
            EnemyExplosion ();
        }
    }

    void EnemyExplosion () {
        GameObject effect = GameObject.Instantiate (explosionEffect, transform.position, transform.rotation);
        Destroy (effect, 1);
        Destroy (this.gameObject);
        // TODO 加金钱
    }

}