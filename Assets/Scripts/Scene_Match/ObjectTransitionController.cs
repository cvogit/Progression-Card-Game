using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class ObjectTransitionController : MonoBehaviour {

	public List<ObjectToMove> 	MovingObjects;
	public List<ObjectToMove> 	SmoothObjects;


	void Awake() {
		MovingObjects = new List<ObjectToMove> ();
		SmoothObjects = new List<ObjectToMove> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Move each object in the list with Delta Time
		int count = MovingObjects.Count;

		for (int i = 0; i < count; i++) {
			Vector3 Starting = MovingObjects [i].StartPos;
			Vector3 Ending = MovingObjects [i].EndPos;

			// Get time passed percentage, if greater than 1, set to 1
			MovingObjects [i].PassedTime += Time.deltaTime/MovingObjects [i].Time;
			if (MovingObjects [i].PassedTime >= 1.0f)
				MovingObjects [i].PassedTime = 1.0f;

			// Move the object
			MovingObjects[i].Object.transform.position = Vector3.Lerp(Starting, Ending,MovingObjects [i].PassedTime);
		}
		// If the object reached the ending position, delete the object from the list
		MovingObjects.RemoveAll(Object => Object.PassedTime == 1.0f);

		// Move each object smoothly with smoothstep
		count = SmoothObjects.Count;
		for (int i = 0; i < count; i++) {
			float StartX = SmoothObjects [i].StartPos.x;
			float StartY = SmoothObjects [i].StartPos.y;
			float StartZ = SmoothObjects [i].StartPos.z;

			float EndingX = SmoothObjects [i].EndPos.x;
			float EndingY = SmoothObjects [i].EndPos.y;
			float EndingZ = SmoothObjects [i].EndPos.z;

			SmoothObjects [i].PassedTime += Time.deltaTime/SmoothObjects [i].Time;
			if (SmoothObjects [i].PassedTime >= 1.0f)
				SmoothObjects [i].PassedTime = 1.0f;

			float NewX = Mathf.SmoothStep (StartX, EndingX, SmoothObjects [i].PassedTime);
			float NewY = Mathf.SmoothStep (StartY, EndingY, SmoothObjects [i].PassedTime);
			float NewZ = Mathf.SmoothStep (StartZ, EndingZ, SmoothObjects [i].PassedTime);

			// Move the object
			SmoothObjects[i].Object.transform.position = new Vector3 (NewX, NewY, NewZ);
		}
		// If the object reached the ending position, delete the object from the list
		SmoothObjects.RemoveAll(Object => Object.PassedTime == 1.0f);
	}

	public void AddObjectToMove (GameObject pObject, Vector3 pStartPos, Vector3 pEndPos, float pTime) {
		ObjectToMove tObject = new ObjectToMove (pObject, pStartPos, pEndPos, pTime);
		MovingObjects.Add (tObject);
	}

	public void AddObjectToSmoothStep (GameObject pObject, Vector3 pStartPos, Vector3 pEndPos, float pTime) {
		ObjectToMove tObject = new ObjectToMove (pObject, pStartPos, pEndPos, pTime);
		SmoothObjects.Add (tObject);
	}
}
