using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]	//表面该类可序列化，可直接在Unity上显示操作
public class Wave
{
	public GameObject enemy;
	public int count;
	public float rate;
}
