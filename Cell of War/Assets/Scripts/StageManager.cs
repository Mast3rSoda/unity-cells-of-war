using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
	private GameMaster GM;

	private float enemySpawnRadius = 8f;
    private float enemySpawnSafeDistance = 6f;
    private float enemySpawnCdBase = 0.6f;
    private float enemySpawnCd = 0f;

    private float enemiesCount;


    public int enemySimpleAlive;
    private int enemySimpleCap = 7;
    private int enemySimpleToSpawn;
    private int enemySimpleBase = 3;
    private float enemySimpleScaling = 0.34f;

    public int enemyBigAlive;
    private int enemyBigCap = 2;
    private int enemyBigToSpawn;
    private int enemyBigBase = 0;
    private float enemyBigScaling = 0.15f;

    public int enemySmartAlive;
    private int enemySmartCap = 5;
    private int enemySmartToSpawn;
    private int enemySmartBase = 0;
    private float enemySmartScaling = 0.25f;


	public GameObject enemySimple;
	public GameObject enemyBig;
	public GameObject enemySmart;

	private GameObject player;
	private GameObject enemiesContainer;

	public GameObject getReadyPanel;
	private static bool isCountingDown;
	private float countdown;
	private float countdownBase = 3f;
	private float timeToCollectMoney = 5f;
	private float countdownDeathBase = 2f;
	private bool isLevelBeaten;
	private bool isPlayerDead;


	void LoadStats() {
		int stage = GM.stage;

		enemySimpleAlive = enemyBigAlive = enemySmartAlive = 0;

		enemySimpleToSpawn = enemySimpleBase + (int)(enemySimpleScaling * (float)stage);
		enemyBigToSpawn = enemyBigBase + (int)(enemyBigScaling * (float)stage);
		enemySmartToSpawn = enemySmartBase + (int)(enemySmartScaling * (float)stage);

		enemiesCount = enemySimpleToSpawn + enemyBigToSpawn + enemySmartToSpawn;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		LoadStats();

		player = GameObject.FindWithTag("Player");
		enemiesContainer = GameObject.FindWithTag("EnemiesContainer");

		getReadyPanel.SetActive(true);
		isCountingDown = true;
		countdown = countdownBase;
		isLevelBeaten = false;
		isPlayerDead = false;
	}


	Vector2 GetRandomPosition(float radius) {
		return Random.insideUnitCircle * radius;
	}


	void Spawn(GameObject enemy) {
		Vector3 spawnPosition = GetRandomPosition(enemySpawnRadius);
		while (Vector3.Distance(player.transform.position, spawnPosition) < enemySpawnSafeDistance) {
			spawnPosition = GetRandomPosition(enemySpawnRadius);
		}
		GameObject spawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
		spawnedEnemy.transform.parent = enemiesContainer.transform;
	}


	void decideToSpawn() {
		if (enemySimpleAlive < enemySimpleCap && enemySimpleToSpawn > 0) {
			Spawn(enemySimple);
			enemySimpleAlive++;
			enemiesCount--;
			enemySimpleToSpawn--;
			enemySpawnCd = enemySpawnCdBase;
		}
		if (enemyBigAlive < enemyBigCap && enemyBigToSpawn > 0) {
			Spawn(enemyBig);
			enemyBigAlive++;
			enemiesCount--;
			enemyBigToSpawn--;
			enemySpawnCd = enemySpawnCdBase;
		}
		if (enemySmartAlive < enemySmartCap && enemySmartToSpawn > 0) {
			Spawn(enemySmart);
			enemySmartAlive++;
			enemiesCount--;
			enemySmartToSpawn--;
			enemySpawnCd = enemySpawnCdBase;
		}
	}


	void Update() {
		if (!isCountingDown) {
			if (player == null) {
				isCountingDown = true;
				countdown = countdownDeathBase;
				isPlayerDead = true;
			} else {
				if (enemiesCount == 0 && enemiesContainer.transform.childCount == 0) {
					// stage beaten, time to collect moneys
					isCountingDown = true;
					countdown = timeToCollectMoney;
					isLevelBeaten = true;
				} else if (enemySpawnCd <= 0f) {
					decideToSpawn();
				} else {
					enemySpawnCd -= Time.deltaTime;
				}
			}
		} else {
			if (countdown <= 0f) {
				if (isLevelBeaten) {
					// change the scene after victory
					GM.increaseStage();
					SceneManager.LoadScene("Shop");
				} else if (isPlayerDead) {
					SceneManager.LoadScene("MainMenu");
				} else {
					isCountingDown = false;
					getReadyPanel.SetActive(false);
				}
			} else {
				countdown -= Time.deltaTime;
			}
		}
	}
}
