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
	public Animator Anim {
		get;
		set;
	}

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
	//
	public bool IsAlive {
		//
		get{ return health.MyCurrentValue > 0; }
		//set;
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
	public Transform Target {
		get;
		set;
	}
	//
	protected Coroutine attackRoutine;
	// Bool to check if Attacking
	public bool IsAttacking {
		get;
		set;
	}

	// Use this for initialization (beforeStart)
	protected virtual void Start () {

		health.Initialize (initHealthValue, initHealthValue);
		// Find Components
		Anim = GetComponentInChildren <Animator> ();
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
		//
		if (IsAlive) {
			// Move to Direction, with Speed, Normalize to remove diagonally move bonus
			rigid.velocity = direction.normalized * speed;
		}

	}

	// Function to Disable and Enable correct Layers
	public void ActivateLayer (string layerName) {
		// Go through all Layers in Animator
		for (int i = 0; i < Anim.layerCount; i++) {
			// Disable all Layers
			Anim.SetLayerWeight (i, 0);
		}
		// Set the Chosen layer Active
		Anim.SetLayerWeight(Anim.GetLayerIndex(layerName), 1);
	}


	public void HandleLayers () {
		//
		if (IsAlive) {
			//
			if (IsMoving) {
				//
				Anim.SetFloat("DirX", direction.x);
				Anim.SetFloat ("DirY", direction.y);
				//
				ActivateLayer ("MovementLayer");
			}
			else if (IsAttacking) {
				//
				ActivateLayer ("AttackLayer");
			} else {
				//
				ActivateLayer ("IdleLayer");
			}
		//
		} else {
			//
			ActivateLayer ("DeathLayer");
		}

	}


	//
	public virtual void TakeDamage (float damage, Transform damageSource) {
		//
		health.MyCurrentValue -= damage;
		//
		if (health.MyCurrentValue <= 0) {
			//
			Direction = Vector2.zero;
			rigid.velocity = Direction;
			//Die
			Anim.SetTrigger("Die");
		}
	}
}