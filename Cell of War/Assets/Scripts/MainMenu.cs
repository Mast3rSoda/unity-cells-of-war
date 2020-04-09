using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour 
{
	public GameObject menuUI;
    public GameObject settingsMenuUI;
	public GameObject highScoreUI;
	private GameMaster GM;
	private AudioManager AM;
	public Text hSText;
	public Toggle specialEffectsToggle;
	public Slider volumeSlider;
	void Start()
	{
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		int highScore = GM.maxStage;
		if(highScore > 0)
		{
			highScoreUI.SetActive(true);
			hSText.text = highScore.ToString();
		}
		AM = GameObject.FindWithTag("AM").GetComponent<AudioManager>();
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			settingsMenuUI.SetActive(false);
			menuUI.SetActive(true);
        }
	}
	public void Play()
	{
		GM.resetStats();
		GM.increaseStage();
		SceneManager.LoadScene("Intro");
	}
		
	public void loadSettings()
    {
        menuUI.SetActive(false);
		volumeSlider.value = AM.generalVolume;
		specialEffectsToggle.isOn = !AM.areSpecialEffectsMuted;
		if (!specialEffectsToggle.isOn)
		{
			AM.toggleSpecialEffects();
		}
        settingsMenuUI.SetActive(true);
    }
	public void setVolume (float volume)
	{
		AM.setVolume(volume);
	}
	public void toggleSpecialEffects()
	{
		AM.toggleSpecialEffects();
	}
	public void backButton()
    {
        settingsMenuUI.SetActive(false);
        menuUI.SetActive(true);
    }
	
	public void Quit() {
		Application.Quit();
	}
}
