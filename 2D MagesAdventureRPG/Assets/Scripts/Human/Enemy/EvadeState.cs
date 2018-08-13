using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Evade state.
/// 
/// </summary>

public class EvadeState : IState {
	//
	private Enemy parent;


	//
	public void Enter (Enemy parent) {
		// 
		this.parent = parent;
	}


	//
	public void Update () {
		//
		parent.Direction = (parent.StartPosition - parent.transform.position).normalized;
		//
		parent.transform.position = Vector2.MoveTowards (parent.transform.position, parent.StartPosition, parent.Speed * Time.deltaTime);
		//
		float distance = Vector2.Distance (parent.StartPosition, parent.transform.position);

		//
		if (distance <= 0f) {
			//
			parent.ChangeState (new IdleState() );
		}
	}


	//
	public void Exit () {
		// 
		parent.Direction = Vector2.zero;
		parent.Reset ();
	}
}
