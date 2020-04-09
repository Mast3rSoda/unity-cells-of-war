using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
	private GameMaster GM;

	private AudioManager AM;

	private float speed;
	private float magnetDistance;
	private float magnetSpeedMultiplier = 4f;

	private GameObject player;

	private Rigidbody2D rb;


	void loadStats() {
		speed = GM.movementSpeed * magnetSpeedMultiplier;
		magnetDistance = GM.magnetDistance;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();

		AM = GameObject.FindWithTag("AM").GetComponent<AudioManager>();

		player = GameObject.FindWithTag("Player");

		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}


	void Update() {
		if (player != null) {
			if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < magnetDistance) {
				Vector3 coinPosition = this.gameObject.transform.position;
				Vector3 diff = player.transform.position - coinPosition;
				rb.velocity = diff.normalized * speed;
			} else {
				rb.velocity = Vector3.zero;
			}
		}
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GM.increaseHp();

			AM.play("Slurp");

			Destroy(this.gameObject);
		}
	}
}
