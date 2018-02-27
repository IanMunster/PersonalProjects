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

	// Spells objects
	[SerializeField] private GameObject[] spellPrefabs;
	//
	[SerializeField] private Transform[] staffGems;
	//
	private int gemIndex;


	//
	public Transform Target {
		get;
		set;
	}

	//
	[SerializeField] private SightBlock[] sightblocks;


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
		// Store the input
		direction.y = Input.GetAxisRaw ("Vertical");
		direction.x = Input.GetAxisRaw ("Horizontal");


		if (direction.y < -0.1f) {
			gemIndex = 0;
		} else if (direction.y > 0.1f) {
			gemIndex = 3;
		} else if (direction.x < -0.1f) {
			gemIndex = 1;
		} else if (direction.x > 0.1f) {
			gemIndex = 2;
		}

/// Debug & Testing
///
		if (Input.GetKeyDown(KeyCode.I)) {
			health.MyCurrentValue += 10;
			mana.MyCurrentValue += 20;
		} 
		if (Input.GetKeyDown(KeyCode.O)) {
			health.MyCurrentValue -= 10;
			mana.MyCurrentValue -= 20;
		}
	}


	// Second to wait for AttackAnimation
	private float attackWaitSecond = 2.5f; 


	// Test function to Attack
	private IEnumerator Attack (int spellIndex) {



		if (!isAttacking && !IsMoving) {
			//
			isAttacking = true;
			//
			anim.SetBool ("Attack", isAttacking);
			//
			Spell spell = Instantiate (spellPrefabs [spellIndex], staffGems[gemIndex].position, Quaternion.identity).GetComponent<Spell> ();

			spell.Target = Target;
			//
			yield return new WaitForSeconds (attackWaitSecond);
			//
			StopAttack ();

		}

	}


	//
	public void CastSpell (int spellIndex) {
		//
		BlockSight ();
		//
		if (Target != null && !isAttacking && !IsMoving && InLineOfSight()) {
			//
			attackRoutine = StartCoroutine (Attack(spellIndex));
		}
	}


	//
	private bool InLineOfSight () {
		Vector3 targetDirection = (Target.position - transform.position).normalized;

		RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance(transform.position, Target.position), LayerMask.GetMask("SightBlock"));

		if (hit.collider == null) {
			return true;
		}

		//
		return false;
	}


	//
	private void BlockSight () {
		foreach (SightBlock block in sightblocks) {
			block.Deactivate ();
		}

		sightblocks [gemIndex].Activate ();
	}

///
/// End Testing & Debug
}