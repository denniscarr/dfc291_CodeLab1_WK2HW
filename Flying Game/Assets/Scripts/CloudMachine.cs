using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMachine : MonoBehaviour {

	ParticleSystemRenderer psr;

	public Material[] clouds;

	void Start() {
		psr = GetComponent<ParticleSystemRenderer> ();
	}

	void Update() {
		psr.material = clouds [Random.Range (0, clouds.Length)];
	}
}
