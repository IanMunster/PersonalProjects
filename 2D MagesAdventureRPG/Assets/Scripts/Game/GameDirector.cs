using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Game director.
/// 
/// </summary>

public class GameDirector : MonoBehaviour {

	//
	[SerializeField]
	private Player player;

	private NPC currentTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ClickOnTarget ();
	}


	//
	private void ClickOnTarget () {
		//
		if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject()) {

			//Raycast to MouseClick
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask ("Clickable"));

			//
			if (hit.collider != null) {
				//
				if (currentTarget != null) {
					//
					currentTarget.Deselect ();
				}
				//
				currentTarget = hit.collider.GetComponent <NPC> ();
				//
				player.Target = currentTarget.Select ();

				// Show target UI frame
				UIDirector.GetInstance.ShowTargetFrame (currentTarget);

			} else {

				// Hide Target UI frame
				UIDirector.GetInstance.HideTargetFrame ();

				//
				if (currentTarget != null) {
					currentTarget.Deselect ();
				}
				currentTarget = null;
				player.Target = null;
			}
		} 
	}
}
