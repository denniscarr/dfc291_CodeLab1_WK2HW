using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DennisGameManager : MonoBehaviour {

	// How far from the center of the screen things are allowed to move.
	public float moveRangeX, moveRangeY;

	// Health pieces
	public GameObject[] healthPieces;

	public void ClampMe(Transform me)
	{
		Vector3 clampedPos = new Vector3 (
			Mathf.Clamp(me.position.x, -moveRangeX, moveRangeX),
			Mathf.Clamp(me.position.y, -moveRangeY, moveRangeY),
			me.position.z
		);
		me.position = clampedPos;
	}

	public void BounceMe(Transform me)
	{
		Vector3 newVelocity = me.GetComponent<Rigidbody> ().velocity;

		if (newVelocity == null)
			return;
		
		if (me.transform.position.x > moveRangeX || me.transform.position.x < -moveRangeX) {
//			ClampMe (me);
			newVelocity.x *= -1;
		}

		me.GetComponent<Rigidbody> ().velocity = newVelocity;
	}

	public void RemoveHealth(int currentHealth) {
		if (currentHealth < 0) {
			return;
		}
		healthPieces [currentHealth].SetActive (false);
	}
}
