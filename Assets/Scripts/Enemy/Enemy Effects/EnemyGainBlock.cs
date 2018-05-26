using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy gain block.
/// </summary>
public class EnemyGainBlock : _Effect {

	public override IEnumerator ResolveEffect() {

		int tBlock 				= mMatchController.EnemyActing.Block;
		EnemyController tEnemy 	= mMatchController.EnemyActing;

		tEnemy.AddBlock(tBlock);

		yield return "Finished";
	}
}
