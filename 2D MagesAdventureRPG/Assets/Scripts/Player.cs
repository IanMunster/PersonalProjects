using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// 
/// </summary>

public class Player : Character {



	// StatClass, Health of Player
	[SerializeField] private Stat health;
	// StatClass, Mana of Player
	[SerializeField] private Stat mana;


	// Current value of Health
	private float healthValue = 100f;
	// Max value of Health
	/*[SerializeField]*/ private float maxHealth = 100f;

	// Current value of Mana
	private float manaValue = 100f;
	// Max value of Mana
	private float maxMana = 200f;


	// Use this for initialization (Before Start)
	protected override void Awake () {
		
		health.Initialize (healthValue, maxHealth);
		mana.Initialize (manaValue, maxMana);

		base.Awake ();
	}


	// Update is called once per frame
	protected override void Update () {
		GetInput ();
		base.Update ();
	}


	// Function to get Input
	private void GetInput () {
		// Story the input
		direction.y = Input.GetAxisRaw ("Vertical");
		direction.x = Input.GetAxisRaw ("Horizontal");


		/// Debug
		/// 
		if (Input.GetKeyDown(KeyCode.I)) {
			health.MyCurrentValue += 10;
			mana.MyCurrentValue += 20;
		} 
		if (Input.GetKeyDown(KeyCode.O)) {
			health.MyCurrentValue -= 10;
			mana.MyCurrentValue -= 20;
		}
		///
		/// End Debug
	}


	/// Testing
	/// 
	private float attackWaitSecond = 3f; 

	private IEnumerator Attack () {

		//
		yield return new WaitForSeconds (attackWaitSecond);

		//

	}

	// Values for Long Idle Animation
	[SerializeField] private float waitLongMax = 600f;
	private float waitLong;

	// Function to Override Character Animate? And Play Long Idle Animation
	protected override void AnimateMovement () {
		base.AnimateMovement ();
		//
		if (direction == Vector2.zero) {
			waitLong++;
			if (waitLong == waitLongMax) {
				anim.SetTrigger ("LongIdle");
				waitLong = 0;
			}
		} else {
			waitLong = 0;
		}
	}
	/// End Testing
}