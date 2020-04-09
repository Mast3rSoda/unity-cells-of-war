using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour {
	private GameMaster GM;
	private Death death;
	private StageManager SM;

	private float damageToTake;

	private float hp;
	private float hpBase = 3;
	private float hpScaling = 1.06f;

	private float movementSpeed;
	private float movementSpeedBase = 3f;
	private float movementSpeedScaling = 1.03f;


	private float playerPosition;

	private GameObject player;
	private Rigidbody2D rb;


	void loadStats() {
		int stage = GM.stage;
		hp = hpBase * Mathf.Pow(hpScaling, stage - 1);
		movementSpeed = movementSpeedBase * Mathf.Pow(movementSpeedScaling, stage - 1);
		damageToTake = GM.damage;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();

		death = GameObject.FindWithTag("Death").GetComponent<Death>();

		SM = GameObject.FindWithTag("SM").GetComponent<StageManager>();

		player = GameObject.Find("Player");
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}


	void Update() {
		if (player != null) {
			Transform playerTransform = player.transform;
			Vector3 playerPosition = playerTransform.position;
			Vector3 diff = playerPosition - transform.position;
			rb.velocity = diff.normalized * movementSpeed;
		}
	}


	void OnTriggerEnter2D(Collider2D bullet) {
		if (bullet.gameObject.tag == "PlayerBullet") {
			Destroy(bullet.gameObject);
			hp -= damageToTake;
			if (hp <= 0) {
				SM.enemySimpleAlive--;
				death.killEnemy(this.gameObject);
			}
		}
	}
}
