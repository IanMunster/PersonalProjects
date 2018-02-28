using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPC.
/// </summary>

public class NPC : Character {

	//
	public virtual Transform Select () {
		return hitBox;
	}
	//
	public virtual void Deselect() {
		
	}
}
