using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;
		if (tag == "EnemyBullet" || tag == "PlayerBullet") {
			Destroy(other.gameObject);
		}
	}
}
