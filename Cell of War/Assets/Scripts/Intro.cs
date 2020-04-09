using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;

public class Intro : MonoBehaviour {
	private string text = 	"General:\n - " + 
							"A cry for help in time of need awaits relief from the immune system. " + 
							"The virus is growing in power with every second! " + 
							"Your job is to go out there and fight the virus so we have enough time to rebuild the organism. " +
							"Good luck white cell, you're our only hope...";
	private string[] textArray;

	private string displayedText = "";
	private int currWordIndex = 0;

	public Text textContainer;
	public GameObject skipContainer;
	public GameObject nextSceneContainer;

	private float timeBetweenWords = 0f;
	private float timeBetweenWordsBase = 0.037f;

	private bool playerWantsToSkip = false;
	private float skipTimeRequired = 1f;
	private float skipTime = 0f;

	private bool messageEnded = false;


	void Start() {
		textArray = Regex.Split(text, @"");
		textContainer.text = "";
	}


	void Update() {
		if (timeBetweenWords <= 0f) {
			if (currWordIndex < textArray.Length) {
				displayedText += textArray[currWordIndex++];
				timeBetweenWords = timeBetweenWordsBase;
				textContainer.text = displayedText;
			} else {
				messageEnded = true;
				skipContainer.SetActive(false);
				playerWantsToSkip = false;
				nextSceneContainer.SetActive(true);
			}
		} else {
			timeBetweenWords -= Time.deltaTime;
		}

		if (!messageEnded && !playerWantsToSkip && Input.anyKey) {
			playerWantsToSkip = true;
			skipContainer.SetActive(true);
		}

		if (playerWantsToSkip) {
			if (skipTime >= skipTimeRequired) {
				playerWantsToSkip = false;
				SceneManager.LoadScene("Arena");
			}
			if (Input.GetKey(KeyCode.Space)) {
				skipTime += Time.deltaTime;
			} else {
				skipTime = 0f;
			}
		}

		if (messageEnded) {
			if (Input.anyKey) {
				messageEnded = false;
				SceneManager.LoadScene("Arena");
			}
		}
	}
}
