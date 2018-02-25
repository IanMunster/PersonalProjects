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
	// Animator Component for Movement
	protected Animator anim;

	// Rigidbody Component for Movement
	private Rigidbody2D rigid;


	// Use this for initialization (beforeStart)
	protected virtual void Awake () {
		// Find Components
		anim = GetComponent <Animator> ();
		rigid = GetComponent <Rigidbody2D> ();
	}


	// Update is called once per frame
	protected virtual void Update () {
		// Call the Animator Function
		AnimateMovement ();
	}


	// Called every physicStep
	private void FixedUpdate () {
		//Call the MoveFunction
		Move ();
	}


	// Is the Character Moving
	public bool IsMoving {
		get { 
			return direction != Vector2.zero; 
		}
	}

	// Function to Move
	public void Move () {
		// Move to Direction, with Speed, Normalize to remove diagonally move bonus
		rigid.velocity = direction.normalized * speed;
	}


	//Function to Animate the Movement
	protected virtual void AnimateMovement () {
		anim.SetFloat("DirX", direction.x);
		anim.SetFloat ("DirY", direction.y);
	}


	// Function to Disable and Enable correct Layers
	public void ActivateLayer (string layerName) {
		// Go through all Layers in Animator
		for (int i = 0; i < anim.layerCount; i++) {
			// Disable all Layers
			anim.SetLayerWeight (i, 0);
		}
		// Set the Chosen layer Active
		anim.SetLayerWeight(anim.GetLayerIndex(layerName), 1);
	}


	public void HandleLayers () {
		
	}
}