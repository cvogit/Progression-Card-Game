using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour {

	public GameObject EnemySpawnSlot;

	public GameObject CreateEnemy(GameObject pEnemyPrefab) {
		// Create the card
		GameObject tEnemy = Instantiate (pEnemyPrefab, EnemySpawnSlot.transform.position, Quaternion.identity);
		tEnemy.transform.SetParent (EnemySpawnSlot.transform);
		tEnemy.transform.position = EnemySpawnSlot.transform.position;
		tEnemy.transform.rotation = EnemySpawnSlot.transform.rotation;
		return tEnemy;
	}
}
