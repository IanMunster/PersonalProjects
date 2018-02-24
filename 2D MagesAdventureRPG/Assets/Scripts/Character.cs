using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// 
/// </summary>

public abstract class Character : MonoBehaviour {


	// Movement speed
	[SerializeField] private float speed;
	// Direction of Movement
	protected Vector2 direction;

	// Animator Component for Movement, in Child object "Sprite"
	private Animator anim;


	// Use this for initialization (beforeStart)
	protected virtual void Awake () {
		//
		anim = GetComponent <Animator> ();
	}


	// Update is called once per frame
	protected virtual void Update () {
		//Call the MoveFunction
		Move ();
		AnimateMovement ();
	}


	// Function to Move
	public void Move () {
		// Translate the Transform to Direction, with Speed per Frame
		transform.Translate (direction * speed * Time.deltaTime);
	}


	//Function to Animate the Movement
	public void AnimateMovement () {
		anim.SetFloat("DirX", direction.x);
		anim.SetFloat ("DirY", direction.y);
	}

}