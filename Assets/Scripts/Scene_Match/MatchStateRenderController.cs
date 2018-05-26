using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchStateRenderController : MonoBehaviour {

	public MatchHelperController MatchHelper;

	public Text Mana;
	public Text MaxMana;

	private GameObject Player;

	private int PlayerMaxHealth;
	private int PlayerMaxMana;

	private int PlayerHealth;
	private int PlayerMana;
	private int PlayerDeckSize;

	private int EnemyMaxHealth;
	private int EnemyMaxMana;

	private int EnemyHealth;
	private int EnemyMana;
	private int EnemyDeckSize;

	void Awake() {
		PlayerHealth = 40;
		PlayerMana = 1;
		PlayerDeckSize = 30;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayerHealth(int pHealth) {
		PlayerHealth = pHealth;
	}

	public void ChangePlayerHealth(int pHealth) {
		PlayerHealth += pHealth;
	}

	public void SetPlayerMaxHealth(int pMaxHealth) {
		PlayerMaxHealth = pMaxHealth;
	}
}
