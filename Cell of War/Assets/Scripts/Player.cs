using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
 
public class Player : MonoBehaviour {
	private GameMaster GM;

	private float movementSpeed;

	private AudioManager AM;

	private Vector3 mousePosition;
	private Rigidbody2D rb;

	public GameObject shield;

	private float immuneTimeStart = 1.4f;
	private float immuneTime = 0f;


	void loadStats() {
		movementSpeed = GM.movementSpeed;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();

		AM = GameObject.FindWithTag("AM").GetComponent<AudioManager>();

		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}


	void FixedUpdate() {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 diff = mousePosition - transform.position;
		rb.velocity = diff.normalized * movementSpeed;

		if (immuneTime >= 0f) {
			immuneTime -= Time.fixedDeltaTime;
		} else if (immuneTime < 0f && shield.activeSelf) {
			shield.SetActive(false);
		}
	}


	void checkIfAlive() {
		if (GM.currHp == 0) {
			Destroy(this.gameObject);
		}
	}


	void takeDamage() {
		if (immuneTime <= 0f) {
			GM.currHp--;
			AM.play("Grunt");
			checkIfAlive();
			immuneTime = immuneTimeStart;
			shield.SetActive(true);
		}
	}


	void OnTriggerEnter2D(Collider2D bullet) {
		switch (bullet.gameObject.tag) {
			case "EnemyBullet":
				Destroy(bullet.gameObject);
				takeDamage();
				break;
			default:
				break;
		}
	}


	void OnCollisionEnter2D(Collision2D other) {
		switch (other.gameObject.tag) {
			case "Enemy":
				takeDamage();
				break;
			default:
				break;
		}
	}


	void OnCollisionStay2D(Collision2D other) {
		switch (other.gameObject.tag) {
			case "Enemy":
				takeDamage();
				break;
			default:
				break;
		}
	}
}
