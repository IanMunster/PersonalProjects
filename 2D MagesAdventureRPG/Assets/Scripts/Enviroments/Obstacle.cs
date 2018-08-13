using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Obstacle.
/// 
/// </summary>

public class Obstacle : MonoBehaviour, IComparable <Obstacle> {

	//
	public int CompareTo (Obstacle other) {
		//
		if (GetSpriteRenderer.sortingOrder > other.GetSpriteRenderer.sortingOrder) {
			//
			return 1;
			//
		} else if (GetSpriteRenderer.sortingOrder < other.GetSpriteRenderer.sortingOrder) {
			//
			return -1;
		}
		//
		return 0;
	}

	//
	public SpriteRenderer GetSpriteRenderer { get; set; }

	//
	private Color defaultColor;
	private Color fadedColor;


	// Use this for initialization
	void Start () {
		GetSpriteRenderer = GetComponent <SpriteRenderer> ();
		defaultColor = GetSpriteRenderer.color;
		fadedColor = defaultColor;
		fadedColor.a = 0.75f;
	}


	//
	public void FadeOut () {
		//
		GetSpriteRenderer.color = fadedColor;
	}

	public void FadeIn () {
		//
		GetSpriteRenderer.color = defaultColor;
	}
}
