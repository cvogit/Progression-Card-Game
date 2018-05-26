using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public Text HealthText;
	public Text BlockText;

	public string Affiliation;
	public string EnemyName;
	public int 	  Health;
	public int MaxHealth;
	public int Damage;
	public int Block;

	public List<string> SkillEffects;
	public List<string> OnTurnStartEffects;
	public List<string> OnTurnEndEffects;
	public List<string> OnHitEffects;
	public List<string> OnAttackEffects;

	private MatchController mMatchController;
	public bool SkipTurn;

	public List<_Effect> mSkillEffects;
	public List<_Effect> mOnTurnStartEffects;
	public List<_Effect> mOnTurnEndEffects;
	public List<_Effect> mOnHitEffects;
	public List<_Effect> mOnAttackEffects;

	void Awake() {
		mMatchController = GameObject.Find ("Match Controller").GetComponent<MatchController> ();
		SetEffects ();
		SkipTurn = false;
	}

	public IEnumerator AttackedForValue(int pDamage) {
		string Result;

		// TODO activate all OnHitEffects
		CoroutineData EffectCoroutine = new CoroutineData(this, ActivateOnHitEffects () );

		yield return EffectCoroutine.coroutine;

		Result = null;
		Result = (string)EffectCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

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

	public IEnumerator ActivateSkillEffects() {
		string Result;

		CoroutineData EffectCoroutine = new CoroutineData(this, mSkillEffects[Random.Range( 0, mSkillEffects.Count )].ResolveEffect () );

		yield return EffectCoroutine.coroutine;

		Result = null;
		Result = (string)EffectCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}

	public IEnumerator ActivateOnTurnStartEffects() {
		string Result;

		for (int i = 0; i < OnHitEffects.Count; i++) {


			CoroutineData EffectCoroutine = new CoroutineData(this, mOnTurnStartEffects[i].ResolveEffect () );

			yield return EffectCoroutine.coroutine;

			Result = null;
			Result = (string)EffectCoroutine.result;

			yield return new WaitUntil(() => Result == "Finished");
		}

		yield return "Finished";
	}

	public List<_Effect> GetOnHitEffects() {
		_Effect tEffect;
		List<_Effect> Result = new List<_Effect> ();

		for (int i = 0; i < OnHitEffects.Count; i++) {

			tEffect = (_Effect) System.Activator.CreateInstance(null, OnHitEffects[i]).Unwrap();
			Result.Add (tEffect);
		}

		return Result;
	}

	public IEnumerator StartEnemyTurn() {

		// On turn start effects
		string Result;
		CoroutineData EffectCoroutine;

		EffectCoroutine = new CoroutineData(this, ActivateOnTurnStartEffects() );

		yield return EffectCoroutine.coroutine;

		Result = null;
		Result = (string)EffectCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");
		ResetBlock ();

		if (!SkipTurn) {
			// Skill effects
			EffectCoroutine = new CoroutineData (this, ActivateSkillEffects ());

			yield return EffectCoroutine.coroutine;

			Result = null;
			Result = (string)EffectCoroutine.result;

			yield return new WaitUntil (() => Result == "Finished");
			yield return "Finished";
		} else {
			SkipTurn = false;
		}

		yield return "Finished";
	}

	public void RenderHealth() {
		HealthText.text = Health.ToString();
	}

	public void AddBlock(int pBlock) {
		Block += pBlock;
		RenderBlock ();
	}

	public bool IsDead() {
		if (Health <= 0) {
			return true;
		}
		return false;
	}

	public void SetEffects() {
		string Result;
		_Effect tEffect;
		List<_Effect> Effects = new List<_Effect> ();

		// Set on turn start effects
		for (int i = 0; i < OnTurnStartEffects.Count; i++) {

			tEffect = (_Effect) System.Activator.CreateInstance(null, OnTurnStartEffects[i]).Unwrap();

			Effects.Add (tEffect);
		}
		mOnTurnStartEffects = Effects;


		// Set skill effects
		Effects = new List<_Effect> ();
		for (int i = 0; i < SkillEffects.Count; i++) {

			tEffect = (_Effect) System.Activator.CreateInstance(null, SkillEffects[i]).Unwrap();

			Effects.Add (tEffect);
		}
		mSkillEffects = Effects;
	}

	public void ResetBlock() {
		Block = 0;
		RenderBlock ();
	}

	public void RenderBlock() {
		if(Block > 0)
			BlockText.text = Block.ToString();
		else
			BlockText.text = "";
	}
}
