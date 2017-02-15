using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

	public float velocityDecay = 0.9f;
	public float topSpeed = 5f;

	Rigidbody rb;
	Animator animator;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		// Movement
		Vector3 newVelocity = rb.velocity * velocityDecay;
		newVelocity.x += Input.GetAxis ("Horizontal");
		newVelocity.y += Input.GetAxis ("Vertical");
		newVelocity.x = Mathf.Clamp (newVelocity.x, -topSpeed, topSpeed);
		newVelocity.y = Mathf.Clamp (newVelocity.y, -topSpeed, topSpeed);
		rb.velocity = newVelocity;

		// Animation
		animator.SetFloat("x", MyMath.Map(rb.velocity.x, -topSpeed, topSpeed, -1f, 1f));
		animator.SetFloat("y", MyMath.Map(rb.velocity.y, -topSpeed, topSpeed, -1f, 1f));
	}
}
