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

				// Give points
				if (!dead) {gm.bouncingBoys ++;}
				numberOfTimesBounced ++;
				gm.Score += numberOfTimesBounced * gm.bouncingBoys;
				gm.PopupScore (transform.position, numberOfTimesBounced * gm.bouncingBoys);

				// Bounce the enemy's corpse
				GetComponent<Rigidbody> ().isKinematic = false;
				GetComponent<Rigidbody> ().useGravity = true;
				GetComponent<Rigidbody> ().velocity = Vector3.up * 15f;
				GetComponent<Rigidbody> ().AddForce (Vector3.back * 2.5f, ForceMode.Impulse);
				Vector3 randomBounce = Vector3.zero;
				randomBounce.x += Random.Range (-1f, 1f);
				GetComponent<Rigidbody> ().AddForce (randomBounce*5f, ForceMode.Impulse);
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

	int numberOfTimesBounced = 0;

	// Movement stuff
	float noiseOffset;
	public float noiseSpeed = 0.01f;
	float noiseTime = 0.0f;
	public float zSpeedMax = 100f;
	public float zSpeedMin = 5f;

	// Shooting stuff
	public GameObject bullet;
	public float minShotTime = 2.5f;
	public float maxShotTime = 6f;
	float nextShotTime;
	float timeSinceLastShot;

	public GameObject explosion;
	public GameObject deathParticles;

	DennisGameManager gm;
	Rigidbody rb;

	void Start() {
		health = maxHealth;

		noiseOffset = Random.Range (-100f, 100f);

		nextShotTime = Random.Range (minShotTime, maxShotTime);
		timeSinceLastShot = 0.0f;

		gm = GameObject.Find ("Dennis Game Manager").GetComponent<DennisGameManager> ();
		rb = GetComponent<Rigidbody> ();
	}

	void Update()
	{
		if (!dead)
		{
			// See if it's time to shoot
			if (timeSinceLastShot >= nextShotTime) {
				Instantiate (bullet, transform.position, Quaternion.identity);
				nextShotTime = Random.Range (minShotTime, maxShotTime);
				timeSinceLastShot = 0.0f;
			} else {
				timeSinceLastShot += Time.deltaTime;
			}

			// Get zSpeed based on distance from player
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

		// If dead...
		else {
			gm.BounceMe (transform);
		}

		// Destroy me if I leave the screen
		if (transform.position.z < -25f || transform.position.y < -25f) {
			gm.bouncingBoys--;
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player" && !dead) {
			// Hurt player and destroy self
			collider.gameObject.GetComponent<SpaceShip> ().Health -= 1;
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
