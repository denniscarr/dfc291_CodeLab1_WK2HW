using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRandomly : MonoBehaviour {

	void Update ()
	{
		Quaternion newRotation = Random.rotation;
		transform.rotation = newRotation;
	}
}
