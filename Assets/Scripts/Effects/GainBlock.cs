using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Gain block.
/// </summary>
public class GainBlock : _Effect {

	public override IEnumerator ResolveEffect() {

		int tBlock = mMatchController.CardBeingPlay.block;
		PlayerCharacterController tPlayer = mMatchController.Player;

		string Result;

		CoroutineData GainBlockCoroutine = new CoroutineData(mMatchController, tPlayer.AddBlock(tBlock) );

		yield return GainBlockCoroutine.coroutine;

		Result = null;
		Result = (string)GainBlockCoroutine.result;

		yield return new WaitUntil(() => Result == "Finished");

		yield return "Finished";
	}
}
