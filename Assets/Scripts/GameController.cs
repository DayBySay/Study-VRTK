using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject[] EnemyTypes;
	public GameObject spawnedEnemies;
	public GameObject points;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawnEnemyIfNeeded();
	}

	void spawnEnemyIfNeeded() {
		if (spawnedEnemies.transform.childCount > 0)
		{
			return;
		}

		spawnEnemy();
	}

	void spawnEnemy() {
		GameObject enemy = (GameObject) Instantiate(randomEnemy(), spawnPosition(), Quaternion.identity);
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		enemy.GetComponent<EnemyController>().SetTarget(player.transform);
		enemy.transform.parent = spawnedEnemies.transform;
	}

	GameObject randomEnemy() {
		int random = Random.Range (0, EnemyTypes.Length);
		return EnemyTypes[random];
	}
	 Vector3 spawnPosition() {
		 int random = Random.Range(0, points.transform.childCount);
		 Transform point = points.transform.GetChild(random);
		return point.position;
	}
}
