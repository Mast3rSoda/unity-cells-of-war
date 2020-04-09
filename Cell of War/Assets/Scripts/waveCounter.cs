using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveCounter : MonoBehaviour
{
    private GameMaster GM;

	private int wave;

	private Text waveText;


	void loadStats() {
		wave = GM.stage;
	}
	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();
		waveText = this.gameObject.GetComponent<Text>();
        waveText.text = wave.ToString();
	}
}
