using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	private GameMaster GM;

	private int stage;

	private float chancesForHeart = 10f;

	private GameObject miscContainer;

	public GameObject coinPrefab;
	public GameObject heartPrefab;


	void loadStats() {
		stage = GM.stage;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();

		miscContainer = GameObject.FindWithTag("MiscContainer");
	}


	public void killEnemy(GameObject enemy) {
		GameObject coin = Instantiate(coinPrefab, enemy.transform.position, Quaternion.identity);
		coin.transform.parent = miscContainer.transform;

		if (Random.Range(1f, 100f) <= chancesForHeart) {
			enemy.transform.position += new Vector3(0.5f, 0f, 0f);
			GameObject heart = Instantiate(heartPrefab, enemy.transform);
			heart.transform.parent = miscContainer.transform;
		}

		Destroy(enemy);
	}
}
