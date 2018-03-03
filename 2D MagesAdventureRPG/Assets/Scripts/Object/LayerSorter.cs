using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Layer sorter.
/// 
/// </summary>

public class LayerSorter : MonoBehaviour {

	//
	private SpriteRenderer sRenderer;
	//
	private List <Obstacle> obstacles = new List <Obstacle> ();


	// Use this for initialization
	void Start () {
		sRenderer = transform.parent.GetComponentInChildren <SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//
	private void OnTriggerEnter2D (Collider2D collision) {
		//
		if (collision.tag == "Obstacle") {
			//
			Obstacle obstac = collision.GetComponent <Obstacle> ();
			//
			obstac.FadeOut ();
			//
			if (obstacles.Count == 0 || obstac.GetSpriteRenderer.sortingOrder - 1 < sRenderer.sortingOrder) {
				//
				sRenderer.sortingOrder = obstac.GetSpriteRenderer.sortingOrder - 1;
			}

			//
			obstacles.Add (obstac);
		}

	}


	//
	private void OnTriggerExit2D (Collider2D collision) {
		//
		if (collision.tag == "Obstacle") {
			//
			Obstacle obstac = collision.GetComponent <Obstacle> ();
			//
			obstac.FadeIn ();
			//
			obstacles.Remove (obstac);
			//
			if (obstacles.Count == 0) {
				//
				sRenderer.sortingOrder = 200;
			//
			} else {
				//
				obstacles.Sort ();
				//
				sRenderer.sortingOrder = obstacles [0].GetSpriteRenderer.sortingOrder - 1;
			}
		}
	}
}
