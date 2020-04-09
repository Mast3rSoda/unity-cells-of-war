using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHearts : MonoBehaviour {
	private GameMaster GM;

	public Image[] hearts;
	public Sprite emptyHeart;
	public Sprite fullHeart;


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();

		for (int i = 0; i < hearts.Length; i++) {
			hearts[i].enabled = i < GM.maxHp ? true : false;
		}
	}


	void Update() {
		for (int i = 0; i < GM.maxHp; i++) {
			hearts[i].sprite = i < GM.currHp ? fullHeart : emptyHeart;
		}
	}
}
