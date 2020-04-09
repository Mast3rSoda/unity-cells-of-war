using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigGun : MonoBehaviour {
	private float bulletSpeed = 5f;
	private float startTimeBtwShots = 1f;
	private float timeBtwShots = 0.5f;

    private int bulletsCount = 8;

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
            Vector3 angle = new Vector3(0f, 0f, 0f);
			for (int i = 0; i < bulletsCount; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                bullet.transform.parent = bulletsContainer.transform;
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.transform.right * bulletSpeed, ForceMode2D.Impulse);

                angle.z += 360f / (float)bulletsCount;
                firePoint.transform.eulerAngles = angle;
            }
			timeBtwShots = startTimeBtwShots;
		} else {
			timeBtwShots -= Time.deltaTime;
		}
	}
}
