using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;

	public float spawnDelayStart = 5f;
	public float spawnDelayDecrease = 0.001f;
	float spawnDelay;
	float timeSinceLastSpawn;

	DennisGameManager gm;

	void Start() {
		spawnDelay = spawnDelayStart;
		timeSinceLastSpawn = spawnDelayStart-1f;

		gm = GameObject.Find ("Dennis Game Manager").GetComponent<DennisGameManager> ();
	}
	
	void Update ()
	{
		timeSinceLastSpawn += Time.deltaTime;

		spawnDelay -= spawnDelayDecrease * Time.deltaTime;

		if (timeSinceLastSpawn >= spawnDelay) {
			SpawnEnemy ();
			timeSinceLastSpawn = 0.0f;
		}
	}

	void SpawnEnemy() {
		Instantiate (enemy, transform.position, Quaternion.identity);
	}
}
