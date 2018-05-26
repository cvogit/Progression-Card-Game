using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player character controller.
/// </summary>
public class PlayerCharacterController : MonoBehaviour {

	private int MANA_LIMIT = 10;

	public Text HealthText;
	public Text ManaText;
	public Text MaxManaText;
	public Text BlockText;

	public int Level		{ get; set; }

	public int Health 		{ get; set; }
	public int MaxHealth 	{ get; set; }

	public int Mana 		{ get; set; }
	public int MaxMana 		{ get; set; }

	public int Block 		{ get; set; }

	public List<string> OnTurnStart;
	public List<string> OnTurnEnd;
	public List<string> OnHitEffects;
	public List<string> OnAttack;

	public bool SkipTurn;

	private MatchController mMatchController;

	void Awake() {
		mMatchController = GameObject.Find ("Match Controller").GetComponent<MatchController> ();
		SkipTurn = false;
	}

	public void SetPlayer(Player_Data pPlayer) {
		Level = pPlayer.PlayerLevel;
		HealthText.text = pPlayer.Health.ToString ();
		Health = pPlayer.Health;

		Mana 	= 4;
		MaxMana = 4;

		MaxManaText.text = MaxMana.ToString();
		ManaText.text = Mana.ToString ();
	}

	/// <summary>
	/// Starts the player turn.
	/// </summary>
	public void StartPlayerTurn() {
		ResolvePlayerOnTurnStart ();

		ResetBlock ();

		// Increase max mana if it haven't reach the limit
		if (MaxMana < MANA_LIMIT)
			MaxMana++;

		// Reset current turn mana
		Mana = MaxMana;

		// Render the texts
		MaxManaText.text = MaxMana.ToString();
		ManaText.text = Mana.ToString ();

		if (SkipTurn) {
			mMatchController.EndPlayerTurn ();
			SkipTurn = false;
		}
	}

	public void PayManaCost(int pManaCost) {
		Mana -= pManaCost;
		ManaText.text = Mana.ToString ();
	}

	/// <summary>
	/// Resolves the player on turn start effects.
	/// </summary>
	public void ResolvePlayerOnTurnStart() {

	}

	public IEnumerator AddBlock(int pBlock) {
		Block += pBlock;
		RenderBlock ();
		yield return "Finished";
	}

	public void ResetBlock() {
		Block = 0;
		RenderBlock ();
	}

	public IEnumerator AttackedForValue(int pDamage) {
		string Result;
		Debug.Log ("Player taking damage: " + pDamage);

		// TODO activate all OnHitEffects
		CoroutineData EffectCoroutine = new CoroutineData(this, ActivateOnHitEffects () );

		yield return EffectCoroutine.coroutine;

		Result = null;
		Result = (string)EffectCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");
		Debug.Log ("Player start health: " + Health);

		// Deal damage to block value first
		if (Block >= pDamage) {
			Block -= pDamage;
			RenderBlock ();
		} else {
			// Deal the damage
			Health -= (pDamage - Block);
			Block = 0;
			RenderHealth ();
			RenderBlock ();
		}

		if (IsDead ())
			mMatchController.RemoveEnemy (gameObject);


		yield return "Finished";
	}

	public IEnumerator ActivateOnHitEffects() {
		string Result;
		_Effect tEffect;
		List<_Effect> Effects = new List<_Effect> ();

		for (int i = 0; i < OnHitEffects.Count; i++) {

			tEffect = (_Effect) System.Activator.CreateInstance(null, OnHitEffects[i]).Unwrap();

			CoroutineData EffectCoroutine = new CoroutineData(this, tEffect.ResolveEffect () );

			yield return EffectCoroutine.coroutine;

			Result = null;
			Result = (string)EffectCoroutine.result;

			yield return new WaitUntil(() => Result == "Finished");
		}

		yield return "Finished";
	}

	public void RenderHealth() {
		HealthText.text = Health.ToString();
	}

	public void RenderBlock() {
		if(Block > 0)
			BlockText.text = Block.ToString();
		else
			BlockText.text = "";
	}

	public bool IsDead() {
		if (Health <= 0)
			return true;
		return false;
	}
}
