using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DennisGameManager : MonoBehaviour {

	// How far from the center of the screen things are allowed to move.
	public float moveRangeX, moveRangeY;

	// Number of enemies being bounced
	public int bouncingBoys;

	// Score stuff
	public int score = 0;
	public int Score {
		get { return score; }
		set { score = value; scoreText.text = "Score: " + score; }
	}
	Text scoreText;
	public GameObject scorePopup;

	// Health pieces
	public GameObject[] healthPieces;


	void Start() {
		scoreText = GameObject.Find ("Score Text").GetComponent<Text> ();
	}

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

	public void PopupScore(Vector3 position, int number)
	{
		Vector3 popupPos = Camera.main.WorldToScreenPoint (position);
		popupPos.x = MyMath.Map (popupPos.x, 0, Camera.main.pixelWidth, -moveRangeX, moveRangeX);
		popupPos.y = MyMath.Map (popupPos.y, 0, Camera.main.pixelHeight, -moveRangeY, moveRangeY);
		popupPos.z = -4.6f;
		popupPos += Random.insideUnitSphere * 0.5f;
		GameObject popup = (GameObject)Instantiate (scorePopup, popupPos, Quaternion.identity);
		popup.GetComponent<TextMesh> ().text = number.ToString();
		popup.GetComponent<TextMesh> ().color = Random.ColorHSV (0f, 1f, 0f, 1f, 0.9f, 1f, 1f, 1f);
	}
}
