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
	public float AttackRange {
		get;
		set;
	}
	//
	public float AttackCooldownTime {
		get;
		set;
	}
	//
	private IState currentState;


	//
	protected void Awake () {
		AttackRange = 1f;
		ChangeState (new IdleState());
	}

	//
	protected override void Update () {
		//
		if (IsAlive) {
			//
			if (!IsAttacking) {
				//
				AttackCooldownTime += Time.deltaTime;
			}
			//
			currentState.Update ();
		}

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