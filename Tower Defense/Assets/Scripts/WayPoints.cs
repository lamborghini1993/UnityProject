using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{

	public static Transform[] positons;

	private void Awake()
	{
		positons = new Transform[transform.childCount];
		for (int i = 0; i < positons.Length; i++)
		{
			positons[i] = transform.GetChild(i);
		}
	}
}
