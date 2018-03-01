using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public delegate void HealthChanged (float health);
//
public delegate void CharacterRemoved ();

/// <summary>
/// NPC.
/// </summary>
public class NPC : Character {

	//
	public event HealthChanged healthChanged;
	//
	public event CharacterRemoved characterRemoved;


	//
	[SerializeField]
	private Sprite portrait;
	//
	public Sprite GetPortrait {
		get {
			return portrait;
		}
	}


	//
	public virtual Transform Select () {
		return hitBox;
	}
	//
	public virtual void Deselect() {
		//
		healthChanged -= new HealthChanged (UIDirector.GetInstance.UpdateTargetFrame);
		characterRemoved -= new CharacterRemoved (UIDirector.GetInstance.HideTargetFrame);
	}


	//
	public void OnHealthChanged (float health) {
		//
		if (healthChanged != null) {
			healthChanged (health);
		}
	}


	public void OnCharacterRemoved () {
		//
		if (characterRemoved != null) {
			characterRemoved ();
		}

		Destroy (gameObject);
	}
}
