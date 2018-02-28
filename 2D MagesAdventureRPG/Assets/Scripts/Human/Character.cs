﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// 
/// </summary>

[RequireComponent (typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour {

	//
	[SerializeField] protected Stat health;
	// Initial value of Health
	[SerializeField] private float initHealthValue;

	// Movement speed
	[SerializeField] private float speed;
	// Rigidbody Component for Movement
	private Rigidbody2D rigid;


	// Direction of Movement
	protected Vector2 direction;
	// Animator Component for Movement
	protected Animator anim;
	//
	protected Coroutine attackRoutine;
	// Bool to check if Attacking
	protected bool isAttacking = false;
	//
	[SerializeField] protected Transform hitBox;



	// Use this for initialization (beforeStart)
	protected virtual void Awake () {

		health.Initialize (initHealthValue, initHealthValue);
		// Find Components
		anim = GetComponentInChildren <Animator> ();
		rigid = GetComponent <Rigidbody2D> ();
	}


	// Update is called once per frame
	protected virtual void Update () {
		// Call the Animator Function
		HandleLayers ();
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
		if (IsMoving) {
			StopAttack ();

			anim.SetFloat("DirX", direction.x);
			anim.SetFloat ("DirY", direction.y);
			ActivateLayer ("MovementLayer");
		}
		else if (isAttacking) {
			ActivateLayer ("AttackLayer");
		} else {
			ActivateLayer ("IdleLayer");
		}
	}

	//
	public virtual void StopAttack () {
		if (attackRoutine != null) {
			StopCoroutine (attackRoutine);
			//
			isAttacking = false;
			//
			anim.SetBool ("Attack", isAttacking);
		}
	}


	//
	public virtual void TakeDamage (float damage) {
		//
		health.MyCurrentValue -= damage;

		if (health.MyCurrentValue <= 0) {
			//Die
		}
	}
}