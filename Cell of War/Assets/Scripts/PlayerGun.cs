using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour {
	private GameMaster GM;

	private AudioManager AM;

	private float bulletSpeed = 25f;
	private float startTimeBtwShots;
	private float timeBtwShots = 0f;

	private float gunRotationOffest = 180f;
	public GameObject firePoint;
	public GameObject bulletPrefab;
	public GameObject weapon;

	private GameObject bulletsContainer;

	private static SpriteFlippingManager flippingScript;


	void loadPlayerStats() {
		startTimeBtwShots = 1f / GM.fireRate;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadPlayerStats();

		AM = GameObject.FindWithTag("AM").GetComponent<AudioManager>();

		bulletsContainer = GameObject.FindWithTag("BulletsContainer");
		flippingScript = this.gameObject.GetComponent<SpriteFlippingManager>();
	}


	public GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float currDistance = diff.sqrMagnitude;
			if (currDistance < distance) {
				closest = go;
				distance = currDistance;
			}
		}
		return closest;
	}


	void Update() {
		GameObject enemy = FindClosestEnemy();
		if (enemy != null) {
			Vector3 target = enemy.transform.position;
			Vector3 player = this.gameObject.transform.position;
			Vector3 diff = target - player;
			float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - gunRotationOffest;
			if (flippingScript.flipped) {
				angle += 180f;
			}
			weapon.transform.eulerAngles = new Vector3(0, 0, angle);

			Shoot(target);
		}
	}


	void Shoot(Vector3 target) {
		if (timeBtwShots <= 0f) {
			GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
			SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
			bulletSpriteRenderer.flipX = flippingScript.flipped;
			bullet.transform.parent = bulletsContainer.transform;

			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			float dir = flippingScript.flipped ? 1f : -1f;
			rb.AddForce(dir * firePoint.transform.right * bulletSpeed, ForceMode2D.Impulse);
			timeBtwShots = startTimeBtwShots;

			AM.play("Pew");
		} else {
			timeBtwShots -= Time.deltaTime;
		}
	}
}
