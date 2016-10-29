using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] EnemyTypes;
	public GameObject spawnedEnemies;
	public GameObject points;
	public int maxEnemyCount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnEnemyIfNeeded();
	}

	void spawnEnemyIfNeeded() {
		if (spawnedEnemies.transform.childCount > maxEnemyCount -1)
		{
			return;
		}

		spawnEnemy();
	}

	void spawnEnemy() {
		GameObject enemy = (GameObject) Instantiate(randomEnemy(), randomSpawnPosition(), Quaternion.identity);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		enemy.GetComponent<EnemyController>().SetTarget(player.transform);
		enemy.transform.parent = spawnedEnemies.transform;
	}

	GameObject randomEnemy() {
		int random = Random.Range (0, EnemyTypes.Length);
		return EnemyTypes[random];
	}
	 Vector3 randomSpawnPosition() {
		 int random = Random.Range(0, points.transform.childCount);
		 Transform point = points.transform.GetChild(random);
		return point.position;
	}
}
