using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopup : MonoBehaviour {

	public float fadeSpeed = 0.1f;
	TextMesh textMesh;

	void Start()
	{
		textMesh = GetComponent<TextMesh> ();
	}

	void Update()
	{
		Color newColor = textMesh.color;
		newColor = Color.Lerp (newColor, new Color (newColor.r, newColor.g, newColor.b, 0.0f), fadeSpeed);
		textMesh.color = newColor;
	}

}
