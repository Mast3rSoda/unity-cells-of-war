using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCoins : MonoBehaviour {
	private GameMaster GM;

	private int coins;

	private Text coinsText;


	void loadStats() {
		coins = GM.coins;
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();

		coinsText = this.gameObject.GetComponent<Text>();
	}


	void Update() {
		loadStats();
		coinsText.text = coins.ToString();
	}
}
