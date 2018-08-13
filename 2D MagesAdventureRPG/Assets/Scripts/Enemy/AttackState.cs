using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack state.
/// 
/// </summary>

public class AttackState : IState {

	//
	private Enemy parent;
	//
	private float cooldownTime = 1.5f;
	//
	[SerializeField]
	private float extraRange = 0.1f;

	//
	public void Enter (Enemy parent) {
		// 
		this.parent = parent;
	}


	//
	public void Update () {
		//
		if (parent.AttackCooldownTime >= cooldownTime && !parent.IsAttacking) {
			//
			parent.AttackCooldownTime = 0;
			//
			parent.StartCoroutine (Attack());
		}

		// 
		if (parent.Target != null) {
			//
			float distance = Vector2.Distance (parent.Target.position, parent.transform.position);
			// Check range and Attack

			// Player out of range
			if (distance >= parent.AttackRange + extraRange && !parent.IsAttacking) {
				// 
				parent.ChangeState (new FollowState() );
			}
		//
		} else {
			//
			parent.ChangeState (new IdleState() );
		}
	}


	//
	public IEnumerator Attack () {
		//
		parent.IsAttacking = true;
		//
		parent.Anim.SetTrigger ("Attack");
		//
		yield return new WaitForSeconds (parent.Anim.GetCurrentAnimatorStateInfo(2).length);
		//
		parent.IsAttacking = false;
	}

	//
	public void Exit () {
		// 

	}
}
