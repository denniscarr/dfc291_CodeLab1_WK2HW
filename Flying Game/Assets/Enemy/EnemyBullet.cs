using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public float speedMin = 100f;
	public float speedMax = 500f;

	public GameObject explosion;

	Vector3 targetPos;
	Vector3 targetDir;

	Rigidbody rb;
	MeshRenderer meshRenderer;
	DennisGameManager gm;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		meshRenderer = GetComponentInChildren<MeshRenderer> ();
		gm = GameObject.Find ("Dennis Game Manager").GetComponent<DennisGameManager> ();

		// Target the player's position.
		targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
		targetDir = targetPos - transform.position;
		targetDir.Normalize ();
		transform.rotation = Quaternion.Euler (targetDir);
	}
	
	void Update ()
	{
		// Movement
		float zSpeed = MyMath.Map (
			Vector3.Distance (targetPos, transform.position),
			0f, 500f, speedMin, speedMax
		);

		Vector3 newPos = transform.position;

		newPos += (targetDir * zSpeed) * Time.deltaTime;
//			transform.position.x,
//			transform.position.y,
//			transform.position.z + (targetDir*zSpeed) * Time.deltaTime
//		);

		rb.MovePosition (newPos);

		// Texture Stuff
		float offset = Time.time*-1f;
		meshRenderer.material.SetTextureOffset ("_MainTex", new Vector2 (offset, offset));
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, Quaternion.identity);
			collider.GetComponent<SpaceShip> ().Health -= 1;
		}
	}
}
