using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Health stuff
	public int maxHealth = 10;
	private int health;
	public int Health {
		get {
			return health;
		}
		set {
			health = value;

			// See if I died
			if (health <= 0) {
				Instantiate (explosion, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}

			// Increase redness
			MeshRenderer[] meshRends = GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer meshRend in meshRends) {
				Color newColor = meshRend.material.color;
				newColor = Color.Lerp (newColor, Color.red, 1f/(float)maxHealth);
				meshRend.material.color = newColor;
			}
		}
	}

	public GameObject explosion;

	void Start() {
		health = maxHealth;
	}
}
