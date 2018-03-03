using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character.
/// 
/// </summary>

[RequireComponent (typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour {

	// Rigidbody Component for Movement
	private Rigidbody2D rigid;
	// Animator Component for Movement
	protected Animator anim;
	//
	[SerializeField]
	protected Transform hitBox;


	//
	[SerializeField]
	protected Stat health;
	// Initial value of Health
	[SerializeField]
	private float initHealthValue;
	//
	public Stat GetHealthStat {
		get {
			return health;
		}
	}




	// Movement speed
	[SerializeField]
	protected float speed;
	//
	public float Speed {
		get{ return speed; }
		set{ speed = value; }
	}

	// Direction of Movement
	protected Vector2 direction;
	//
	public Vector2 Direction {
		get{ return direction; }
		set{ direction = value; }
	}



	//
	protected Coroutine attackRoutine;
	// Bool to check if Attacking
	protected bool isAttacking = false;


	// Use this for initialization (beforeStart)
	protected virtual void Start () {

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
			anim.SetTrigger("Die");
		}
	}
}