using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Effect {

	protected MatchController mMatchController;

	public _Effect() {
		mMatchController = GameObject.Find ("Match Controller").GetComponent<MatchController> ();
	}

	public virtual IEnumerator ResolveEffect() {
		Debug.Log ("Default Effect.");
		yield return "Finished";
	}
}
