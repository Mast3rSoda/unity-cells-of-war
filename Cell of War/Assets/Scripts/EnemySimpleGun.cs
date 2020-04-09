using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleGun : MonoBehaviour {
	private float bulletSpeed = 5f;
	private float startTimeBtwShots = 1f;
	private float timeBtwShots = 0.5f;

	public GameObject firePoint;
	public GameObject bulletPrefab;
	
	private GameObject player;
	private GameObject bulletsContainer;


	void Start() {
		player = GameObject.Find("Player");
		bulletsContainer = GameObject.FindWithTag("BulletsContainer");
	}


	void Update() {
		if (player != null) {
			Shoot(player.transform.position);
		}
	}


	void Shoot(Vector3 target) {
		if (timeBtwShots <= 0f) {
			Vector3 entity = this.gameObject.transform.position;
			Vector3 diff = target - entity;
			float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			firePoint.transform.eulerAngles = new Vector3(0, 0, angle);

			GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
			bullet.transform.parent = bulletsContainer.transform;
			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
			rb.AddForce(firePoint.transform.right * bulletSpeed, ForceMode2D.Impulse);
			timeBtwShots = startTimeBtwShots;
		} else {
			timeBtwShots -= Time.deltaTime;
		}
	}
}
