using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour {

	public float velocityDecay = 0.9f;

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
		rb.velocity = newVelocity;

		// Animation
		animator.SetFloat("x", Input.GetAxis("Horizontal"));
		animator.SetFloat("y", Input.GetAxis("Vertical"));
	}
}
