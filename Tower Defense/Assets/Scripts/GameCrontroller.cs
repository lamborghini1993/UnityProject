using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCrontroller : MonoBehaviour
{

	public Wave[] waves;
	public Transform MONSTER_START;
	public float waveTime = 10;
	public static int enemyCount = 0;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(EnemyGenerate());
	}

	public static void EnemyDisappear()
	{
		enemyCount--;
	}

	IEnumerator EnemyGenerate()
	{
		foreach (Wave wave in waves)
		{
			for (int i = 0; i < wave.count; i++)
			{
				GameObject.Instantiate(wave.enemy, MONSTER_START.position, Quaternion.identity);
				enemyCount++;
				if (i != wave.count - 1)
				{
					yield return new WaitForSeconds(wave.rate);
				}
			}
			while (enemyCount > 0)
				yield return 0;
			yield return new WaitForSeconds(waveTime);
		}
	}
}
