using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DennisGameManager : MonoBehaviour {

	// How far from the center of the screen things are allowed to move.
	public float moveRangeX, moveRangeY;

	public void ClampMe(Transform me) {
		Vector3 clampedPos = new Vector3 (
			Mathf.Clamp(me.position.x, -moveRangeX, moveRangeX),
			Mathf.Clamp(me.position.y, -moveRangeY, moveRangeY),
			me.position.z
		);
		me.position = clampedPos;
	}
}
