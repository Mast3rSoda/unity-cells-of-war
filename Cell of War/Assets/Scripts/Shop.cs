using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {
	private GameMaster GM;

	private int maxHp;
    private int hpCostBase = 30;
    private float hpCostScaling = 1.6f;
    public int hpLevel;
    private bool hpMaxedOut = false;

    private float damage;
    private float damageValueScaling = 1.16f;
    private int damageCostBase = 10;
    private float damageCostScaling = 1.45f;
    public int damageLevel;

    private float fireRate;
    private float fireRateValueScaling = 1.08f;
    private int fireRateCostBase = 10;
    private float fireRateCostScaling = 1.35f;
    public int fireRateLevel;

    private float movementSpeed;
    private float movementSpeedValueScaling = 1.02f;
    private int movementSpeedCostBase = 10;
    private float movementSpeedCostScaling = 1.3f;
    public int movementSpeedLevel;

    private float magnetDistance;
    private float magnetDistanceValueIncrease = 6f;
    private int magnetDistanceCostBase = 500;
    private float magnetDistanceCostScaling = 2f;
    private int magnetDistanceLevel;
    private int magnetDistanceMaxLevel = 1;
    private bool magnetDistanceMaxedOut = false;

	public Text coinsText;

	public Button damageButton;
	public Image damageButtonImage;
	public Text damageValueText;
	public Text damageCostText;

	public Button fireRateButton;
	public Image fireRateButtonImage;
	public Text fireRateValueText;
	public Text fireRateCostText;

	public Button movementSpeedButton;
	public Image movementSpeedButtonImage;
	public Text movementSpeedValueText;
	public Text movementSpeedCostText;

	public Button hpButton;
	public Image hpButtonImage;
	public Text hpValueText;
	public Text hpCostText;

	public Button magnetButton;
	public Image magnetButtonImage;
	public Text magnetValueText;
	public Text magnetCostText;


	void loadStats() {
		// stats
		maxHp = GM.maxHp;
		damage = GM.damage;
		fireRate = GM.fireRate;
		movementSpeed = GM.movementSpeed;
		magnetDistance = GM.magnetDistance;

		// levels
		hpLevel = GM.hpLevel;
		damageLevel = GM.damageLevel;
		fireRateLevel = GM.fireRateLevel;
		movementSpeedLevel = GM.movementSpeedLevel;
		magnetDistanceLevel = GM.magnetDistanceLevel;
	}


	void spendMoney(int value) {
		GM.coins -= value;
		coinsText.text = GM.coins.ToString();
		updateAllTexts();
	}


	void changeAlpha(Image image, float value) {
		Color color = image.color;
		color.a = value;
		image.color = color;
	}


	void manageAvailability(Button button, Image image, int cost) {
		if (cost > GM.coins) {
			changeAlpha(image, 0.75f);
			button.interactable = false;
		} else {
			changeAlpha(image, 1f);
			button.interactable = true;
		}
	}


	void updateAllTexts() {
		coinsText.text = GM.coins.ToString();

		int cost = 0;

		// hp
		if (maxHp < 10) {
			hpValueText.text = maxHp + " (" + (maxHp + 1) + ")";
			cost = (int)((float)hpCostBase * Mathf.Pow(hpCostScaling, hpLevel));
			hpCostText.text = cost.ToString();
			manageAvailability(hpButton, hpButtonImage, cost);
		} else if (!hpMaxedOut) {
			hpMaxedOut = true;
			hpValueText.text = "MAX LEVEL";
			changeAlpha(hpButtonImage, 0.75f);
			hpButton.interactable = false;
			hpCostText.text = "";
		}
		
		// damage
		damageValueText.text = damage.ToString("#.0") + " (" + (damage * damageValueScaling).ToString("#.0") + ")";
		cost = (int)((float)damageCostBase * Mathf.Pow(damageCostScaling, damageLevel));
		damageCostText.text = cost.ToString();
		manageAvailability(damageButton, damageButtonImage, cost);

		// fire rate
		fireRateValueText.text = fireRate.ToString("#.0") + " (" + (fireRate * fireRateValueScaling).ToString("#.0") + ")";
		cost = (int)((float)fireRateCostBase * Mathf.Pow(fireRateCostScaling, fireRateLevel));
		fireRateCostText.text = cost.ToString();
		manageAvailability(fireRateButton, fireRateButtonImage, cost);

		// movement speed
		movementSpeedValueText.text = movementSpeed.ToString("#.0") + " (" + (movementSpeed * movementSpeedValueScaling).ToString("#.0") + ")";
		cost = (int)((float)movementSpeedCostBase * Mathf.Pow(movementSpeedCostScaling, movementSpeedLevel));
		movementSpeedCostText.text = cost.ToString();
		manageAvailability(movementSpeedButton, movementSpeedButtonImage, cost);

		// magnet
		if (magnetDistanceLevel < magnetDistanceMaxLevel) {
		//	magnetValueText.text = magnetDistance.ToString() + " (" + (magnetDistance + magnetDistanceValueIncrease) + ")";
			magnetValueText.text = "ACTIVATE";
			cost = (int)((float)magnetDistanceCostBase * Mathf.Pow(magnetDistanceCostScaling, magnetDistanceLevel));
			magnetCostText.text = cost.ToString();
			manageAvailability(magnetButton, magnetButtonImage, cost);
		} else if (!magnetDistanceMaxedOut) {
			magnetDistanceMaxedOut = true;
			magnetValueText.text = "MAX LEVEL";
			changeAlpha(magnetButtonImage, 0.75f);
			magnetButton.interactable = false;
			magnetCostText.text = "";
		}
	}


	public void upgradeHp() {
		int cost = (int)((float)hpCostBase * Mathf.Pow(hpCostScaling, hpLevel));

		GM.currHp++;
		GM.maxHp++;
		maxHp = GM.maxHp;
		GM.hpLevel++;
		hpLevel = GM.hpLevel;

		spendMoney(cost);
	}


	public void upgradeDamage() {
		int cost = (int)((float)damageCostBase * Mathf.Pow(damageCostScaling, damageLevel));

		GM.damage = damage * damageValueScaling;
		damage = GM.damage;
		GM.damageLevel++;
		damageLevel = GM.damageLevel;

		spendMoney(cost);
	}


	public void upgradeFireRate() {
		int cost = (int)((float)fireRateCostBase * Mathf.Pow(fireRateCostScaling, fireRateLevel));

		GM.fireRate = fireRate * fireRateValueScaling;
		fireRate = GM.fireRate;
		GM.fireRateLevel++;
		fireRateLevel = GM.fireRateLevel;

		spendMoney(cost);
	}


	public void upgradeMovementSpeed() {
		int cost = (int)((float)movementSpeedCostBase * Mathf.Pow(movementSpeedCostScaling, movementSpeedLevel));

		GM.movementSpeed = movementSpeed * movementSpeedValueScaling;
		movementSpeed = GM.movementSpeed;
		GM.movementSpeedLevel++;
		movementSpeedLevel = GM.movementSpeedLevel;

		spendMoney(cost);
	}


	public void upgradeMagnet() {
		int cost = (int)((float)magnetDistanceCostBase * Mathf.Pow(magnetDistanceCostScaling, magnetDistanceLevel));

		GM.magnetDistance += magnetDistanceValueIncrease;
		magnetDistance = GM.magnetDistance;
		GM.magnetDistanceLevel++;
		magnetDistanceLevel = GM.magnetDistanceLevel;

		spendMoney(cost);
	}


	void Start() {
		GM = GameObject.FindWithTag("GM").GetComponent<GameMaster>();
		loadStats();
		updateAllTexts();
	}


	public void Continue() {
		SceneManager.LoadScene("Arena");
	}
}
