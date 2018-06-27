using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameCrontroller : MonoBehaviour {

	public Wave[] waves;
	public Transform MONSTER_START;
	public float waveTime = 10;
	private int enemyCount = 0;
	public GameObject gameover;

	public static GameCrontroller Instance;

	private void Awake () {
		Instance = this;
	}

	void Start () {
		gameover.SetActive (false);
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
		while (enemyCount > 0)
			yield return 0;
		GameWin ();
	}

	public void GameOver () {
		gameover.SetActive (true);
		gameover.GetComponentInChildren<Text> ().text = "你输了";
		StopAllCoroutines ();
	}

	public void GameWin () {
		gameover.GetComponentInChildren<Text> ().text = "你赢了";
		gameover.SetActive (true);
		StopAllCoroutines ();
	}

	public void Replay () {
		gameover.SetActive (false);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}