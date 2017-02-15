using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	Rigidbody rb;
	MeshRenderer meshRenderer;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		meshRenderer = GetComponentInChildren<MeshRenderer> ();
	}
	
	void Update ()
	{
		// Movement
		Vector3 newPosition = transform.position;
		newPosition = newPosition + transform.forward * speed * Time.deltaTime;
		rb.MovePosition (newPosition);

		// Texture Stuff
		float offset = Time.time*-1f;
		meshRenderer.material.SetTextureOffset ("_MainTex", new Vector2 (offset, offset));
	}
}
