using UnityEngine;
using System.Collections;

public class ScreenShakeScript : MonoBehaviour {

	Camera camera;
	float shake = 0f;
	float shakeAmount = 0.3f;
	float decreaseFactor = 1.0f;

	Vector3 originalPosition;

	void Start() {
		camera = Camera.main;
		originalPosition = camera.transform.position;
	}

	void Update() {
		if (shake > 0f) {
			camera.transform.position = originalPosition + Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;

		} else {
			shake = 0.0f;
		}
	}

	void IncreaseShake(float increaseAmount) {
		shake += increaseAmount;
	}
}
