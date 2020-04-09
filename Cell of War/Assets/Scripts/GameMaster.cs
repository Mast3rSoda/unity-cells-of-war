using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
	public int currHp = 3;
	public int maxHp = 3;
	public float damage = 1f;
	public float fireRate = 1f;
	public float movementSpeed = 5f;
	public float magnetDistance = 0f;

	public int hpLevel = 0;
	public int damageLevel = 0;
	public int fireRateLevel = 0;
	public int movementSpeedLevel = 0;
	public int magnetDistanceLevel = 0;

	public int stage = 0;
	public int coins = 0;

	public int maxStage = 0;

	public static GameMaster Instance;


	void Awake () {
		if (Instance == null) {
			DontDestroyOnLoad(gameObject);
			Instance = this;
		} else {
			Destroy (gameObject);
		}
	}


	public void increaseHp() {
		currHp = Mathf.Min(currHp + 1, maxHp);
	}


	public void increaseStage() {
		stage++;
		maxStage = Mathf.Max(stage, maxStage);
	}


	public void resetStats() {
		currHp = 3;
		maxHp = 3;
		damage = 1;
		fireRate = 1f;
		movementSpeed = 5f;
		magnetDistance = 0f;

		stage = 0;
		coins = 0;

		hpLevel = 0;
		damageLevel = 0;
		fireRateLevel = 0;
		movementSpeedLevel = 0;
		magnetDistanceLevel = 0;
	}
}
