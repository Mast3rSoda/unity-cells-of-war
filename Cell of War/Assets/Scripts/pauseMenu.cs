using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class pauseMenu : MonoBehaviour
{
	private AudioManager AM;

	public static bool isPaused = false;

	public GameObject pauseMenuUI;
	public GameObject settingsMenuUI;
	public Toggle specialEffectsToggle;
	public Slider volumeSlider;


	void Start()
	{
		AM = FindObjectOfType<AudioManager>();
	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (settingsMenuUI.activeSelf)
			{
				settingsMenuUI.SetActive(false);
				pauseMenuUI.SetActive(true);
			}
			else 
			{
				if (isPaused)
				{
					resume();
				}
				else
				{
					pause();
				}
			}
		}
	}


	public void backToPause()
	{
		settingsMenuUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}


	public void resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
	}


	void pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;

	}


	public void loadMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("MainMenu");
	}


	public void loadSettings()
	{
		pauseMenuUI.SetActive(false);
		volumeSlider.value = AM.generalVolume;
		specialEffectsToggle.isOn = !AM.areSpecialEffectsMuted;
		if (!specialEffectsToggle.isOn)
		{
			AM.toggleSpecialEffects();
		}
		settingsMenuUI.SetActive(true);
	}


	public void backButton()
	{
		settingsMenuUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}


	public void setVolume (float volume)
	{
		AM.setVolume(volume);
	}


	public void toggleSpecialEffects()
	{
		AM.toggleSpecialEffects();
	}
}
