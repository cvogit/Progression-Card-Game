using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToMove {

	public GameObject Object;
	public Vector3 StartPos;
	public Vector3 EndPos;
	public float Time;
	public float PassedTime;

	public ObjectToMove(GameObject pObject, Vector3 pStartPos, Vector3 pEndPos, float pTime) {
		this.Object = pObject;
		this.StartPos = pStartPos;
		this.EndPos = pEndPos;
		this.Time 	= pTime;
		this.PassedTime = 0f;
	}
}
