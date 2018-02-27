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
	[SerializeField] private Player player;


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
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, Mathf.Infinity, LayerMask.GetMask ("Clickable"));
			//
			if (hit.collider != null) {
				//
				if (hit.collider.tag == "Enemy") {
					player.Target = hit.transform.GetChild(1);
				}

			} else {
				// detarget
				player.Target = null;
			}
		} 
	}
}
