using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deal card_data damage to all enemies.
/// </summary>
public class DealDamageToAllEnemies : _Effect {

	public override IEnumerator ResolveEffect() {

		int tDamage = mMatchController.CardBeingPlay.damage;
		List<GameObject> tEnemies = mMatchController.Enemies;

		// Deal damage to all enemies
		for (int i = 0; i < tEnemies.Count; i++) {
			string Result;

			CoroutineData AttackCoroutine = new CoroutineData(mMatchController, tEnemies [i].GetComponent<EnemyController> ().AttackedForValue (tDamage) );

			yield return AttackCoroutine.coroutine;

			Result = null;
			Result = (string)AttackCoroutine.result;

			yield return new WaitUntil(() => Result == "Finished");
		}

		yield return "Finished";
	}
}
