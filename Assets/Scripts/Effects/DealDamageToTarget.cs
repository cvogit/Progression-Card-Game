using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal damage to target.
/// </summary>
public class DealDamageToTarget : _Effect {

	public override IEnumerator ResolveEffect() {

		int tDamage 		= mMatchController.CardBeingPlay.damage;
		GameObject tTarget 	= mMatchController.TargetEnemy;

		string Result;

		CoroutineData AttackCoroutine = new CoroutineData(mMatchController, tTarget.GetComponent<EnemyController> ().AttackedForValue (tDamage) );

		yield return AttackCoroutine.coroutine;

		Result = null;
		Result = (string)AttackCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");


		yield return "Finished";
	}
}
