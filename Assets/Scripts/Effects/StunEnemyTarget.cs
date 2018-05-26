using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stun enemy target.
/// </summary>
public class StunEnemyTarget : _Effect {

	public override IEnumerator ResolveEffect() {

		GameObject tEnemy 	= mMatchController.TargetEnemy;

		tEnemy.GetComponent<EnemyController> ().SkipTurn = true;

		yield return "Finished";
	}
}
