using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// Health stuff
	bool dead;
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
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
				GetComponent<Rigidbody> ().velocity = Vector3.up * 25f;
				GetComponent<Rigidbody> ().AddForce (Vector3.back * 5f, ForceMode.Impulse);
				GetComponent<Rigidbody> ().AddTorque (Random.insideUnitSphere * 50f, ForceMode.Impulse);
				GetComponent<Animator> ().Stop ();
				deathParticles.SetActive (true);
				dead = true;
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

	// Movement stuff
	float noiseOffset;
	public float noiseSpeed = 0.01f;
	float noiseTime = 0.0f;
	public float zSpeedMax = 100f;
	public float zSpeedMin = 5f;

	public GameObject explosion;
	public GameObject deathParticles;

	DennisGameManager gm;
	Rigidbody rb;

	void Start() {
		health = maxHealth;
		noiseOffset = Random.Range (-100f, 100f);
		gm = GameObject.Find ("Dennis Game Manager").GetComponent<DennisGameManager> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		// Get zSpeed based on distance from player
		if (!dead) {
			float zSpeed = MyMath.Map (
				              Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, transform.position),
				              0f, 500f, zSpeedMin, zSpeedMax
			              );

			Vector3 newPos = new Vector3 (
				MyMath.Map (Mathf.PerlinNoise (noiseTime + noiseOffset, 0f), 0f, 1f, -gm.moveRangeX, gm.moveRangeX),
				MyMath.Map (Mathf.PerlinNoise (0f, noiseTime + noiseOffset), 0f, 1f, -gm.moveRangeY, gm.moveRangeY),
				transform.position.z - zSpeed * Time.deltaTime
			);

			rb.MovePosition (newPos);

			noiseTime += noiseSpeed;
		}

		if (transform.position.z < -50f || transform.position.y < -50f) {
			Destroy (gameObject);
		}
	}
}
