using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlippingManager : MonoBehaviour {
	public GameObject entity;
	public GameObject weapon;
	public GameObject firePoint;

	private SpriteRenderer entitySpriteRenderer;
	private SpriteRenderer weaponSpriteRenderer;

	public bool flipped = false;


	void Start() {
		entitySpriteRenderer = entity.GetComponent<SpriteRenderer>();
		weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
	}


	void Update() {
		Vector3 currentEuler = weapon.transform.rotation.eulerAngles;
		float rotation = currentEuler.z;
		if (rotation >= 90f && rotation <= 270f) {
			Flip();
		}
	}


	Vector3 NegateX(Vector3 position) {
		return new Vector3(-position.x, position.y, position.z);
	}


	void Flip() {
		entitySpriteRenderer.flipX = !flipped;
		weaponSpriteRenderer.flipX = !flipped;
		firePoint.transform.localPosition *= -1f;
		flipped = !flipped;
	}
}
