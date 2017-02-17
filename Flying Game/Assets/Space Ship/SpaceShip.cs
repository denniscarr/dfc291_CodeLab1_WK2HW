using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

	DennisGameManager gm;

	// Movement
	public float velocityDecay = 0.9f;
	public float topSpeed = 5f;
	public float accelerationFactor = 2f;

	// Shooting
	public float fireRate = 0.1f;
	public float spread = 0.1f;
	float timeSinceLastShot;
	public GameObject bullet;
	Transform gunTip;

	Rigidbody rb;
	Animator animator;

	void Start()
	{
		gm = GameObject.Find ("Dennis Game Manager").GetComponent<DennisGameManager> ();

		timeSinceLastShot = fireRate;

		rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		gunTip = GameObject.Find ("Gun Tip").transform;
	}

	void Update ()
	{
		// Movement
		Vector3 newVelocity = rb.velocity * velocityDecay;
		newVelocity.x += Input.GetAxis ("Horizontal")*accelerationFactor;
		newVelocity.y += Input.GetAxis ("Vertical")*accelerationFactor;
		newVelocity.x = Mathf.Clamp (newVelocity.x, -topSpeed, topSpeed);
		newVelocity.y = Mathf.Clamp (newVelocity.y, -topSpeed, topSpeed);
		rb.velocity = newVelocity;

		gm.ClampMe (transform);

		// Animation
		animator.SetFloat("x", MyMath.Map(rb.velocity.x, -topSpeed, topSpeed, -1f, 1f));
		animator.SetFloat("y", MyMath.Map(rb.velocity.y, -topSpeed, topSpeed, -1f, 1f));

		// Shooting
		timeSinceLastShot += Time.deltaTime;
		if (Input.GetButton("Space") && timeSinceLastShot >= fireRate)
		{
			Vector3 bulletSpread = gunTip.transform.forward;
			bulletSpread.x += Random.Range (-spread, spread);
			bulletSpread.y += Random.Range (-spread, spread);
			Instantiate (bullet, gunTip.position , Quaternion.Euler(bulletSpread+gunTip.rotation.eulerAngles));
			timeSinceLastShot = 0.0f;
		}
	}
}
