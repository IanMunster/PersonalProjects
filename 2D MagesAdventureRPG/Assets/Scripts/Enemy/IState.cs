using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// IState.
/// 
/// </summary>

public interface IState {

	// Prepare State
	void Enter (Enemy parent);

	// Update State
	void Update ();

	// Exit State
	void Exit ();
}
