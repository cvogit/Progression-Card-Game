using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class _PlayCondition {

	protected MatchController mMatchController;

	public _PlayCondition() {
		mMatchController = GameObject.Find ("Match Controller").GetComponent<MatchController> ();
	}

	public virtual bool IsFulfilled() {
		return true;
	}
}
