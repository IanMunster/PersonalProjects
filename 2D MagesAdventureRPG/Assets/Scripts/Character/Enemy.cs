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
	private IState currentState;
	//
	[SerializeField]
	private float initAttackRange = 1f;
	//
	[SerializeField]
	private float initAggroRange = 4f;

	//
	public Vector3 StartPosition { get; set; }
	//
	public float AttackRange { get; set; }
	//
	public float AttackCooldownTime { get; set; }
	//
	public float AggroRange { get; set; }
	//
	public bool InRange {
		get { return Vector2.Distance (transform.position, Target.position) < AggroRange; }
	}


	//
	protected void Awake () {
		//
		StartPosition = transform.position;
		//
		AggroRange = initAggroRange;
		//
		AttackRange = initAttackRange;
		//
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
		//
		return base.Select ();
	}


	//
	public override void Deselect () {
		//
		healthGroup.alpha = 0;
		//
		base.Deselect ();
	}


	//
	public override void TakeDamage (float damage, Transform damageSource) {
		//
		if (!(currentState is EvadeState)) {
			//
			SetTarget (damageSource);
			//
			base.TakeDamage (damage, damageSource);
			//
			OnHealthChanged (health.MyCurrentValue);
		}
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


	// 
	public void SetTarget (Transform target) {
		//
		if (Target == null && !(currentState is EvadeState)) {
			//
			float distance = Vector2.Distance (transform.position, target.position);
			//
			AggroRange = initAggroRange;
			//
			AggroRange += distance;
			//
			Target = target;
		}
	}


	//
	public void Reset () {
		//
		this.Target = null;
		//
		this.AggroRange = initAggroRange;
		//
		this.GetHealthStat.MyCurrentValue = this.GetHealthStat.MyMaxValue;
		//
		OnHealthChanged (health.MyCurrentValue);
	}
}