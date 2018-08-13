using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Idle state.
/// 
/// </summary>

class IdleState : IState {

	//
	private Enemy parent;


	//
	public void Enter (Enemy parent) {
		// 
		this.parent = parent;
		this.parent.Reset ();
	}


	//
	public void Update () {
		// Change to FollowState if Player inRange
		if (parent.Target != null) {
			//
			parent.ChangeState ( new FollowState() );
		}
	}


	//
	public void Exit () {
		// 

	}
}