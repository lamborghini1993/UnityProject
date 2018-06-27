using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCrontroller : MonoBehaviour {

	public Wave[] waves;
	public Transform MONSTER_START;
	public float waveTime = 10;
	private int enemyCount = 0;
	public Text endTip;

	public static GameCrontroller Instance;

	private void Awake () {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		endTip.enabled = false;
		StartCoroutine (EnemyGenerate ());
	}

	public void EnemyDisappear () {
		enemyCount--;
	}

	IEnumerator EnemyGenerate () {
		foreach (Wave wave in waves) {
			for (int i = 0; i < wave.count; i++) {
				GameObject.Instantiate (wave.enemy, MONSTER_START.position, Quaternion.identity);
				enemyCount++;
				if (i != wave.count - 1) {
					yield return new WaitForSeconds (wave.rate);
				}
			}
			while (enemyCount > 0)
				yield return 0;
			yield return new WaitForSeconds (waveTime);
		}
		GameWin ();
	}

	public void GameOver () {
		endTip.enabled = true;
		endTip.text = "You Fail~";
		StopAllCoroutines ();
	}

	public void GameWin () {
		endTip.enabled = true;
		endTip.text = "You Win~";
		StopAllCoroutines ();
	}
}