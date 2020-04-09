using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private float speed;

	private GameObject player;


	void Start() {
		player = GameObject.Find("Player");
		speed = GameObject.FindWithTag("GM").GetComponent<GameMaster>().movementSpeed;
	}


	void FixedUpdate() {
		if (player != null) {
			Vector2 playerPosition = player.transform.position;
			Vector2 cameraPosition = this.gameObject.transform.position;
			Vector2 diff = playerPosition - cameraPosition;
			transform.Translate (diff.normalized * speed * Time.fixedDeltaTime);
		}
	}
}
