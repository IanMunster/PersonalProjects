using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy.
/// 
/// </summary>

public class Enemy : NPC {

	//
	[SerializeField]
	private CanvasGroup healthGroup;

	//
	private Transform target;
	//
	public Transform Target {
		get { return target; }
		set { target = value; }
	}

	//
	private IState currentState;


	protected void Awake () {
		ChangeState (new IdleState());
	}

	//
	protected override void Update () {
		currentState.Update ();
		//
		base.Update ();
	}


	//
	public override Transform Select () {
		//
		healthGroup.alpha = 1;

		return base.Select ();
	}


	//
	public override void Deselect () {

		healthGroup.alpha = 0;

		base.Deselect ();
	}


	//
	public override void TakeDamage (float damage) {
		//
		base.TakeDamage (damage);

		//
		OnHealthChanged (health.MyCurrentValue);
	}


	//
	public void ChangeState (IState newState) {
		//
		if (currentState != null) {
			//
			currentState.Exit ();
		}

		//
		currentState = newState;

		//
		currentState.Enter (this);
	}
}