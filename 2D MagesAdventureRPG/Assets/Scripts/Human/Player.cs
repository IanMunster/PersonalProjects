using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// 
/// </summary>

public class Player : Character {

	// StatClass, Mana of Player
	[SerializeField]
	private Stat mana;

	// Initial value of Mana
	private float initManaValue = 100f;
	//
	private SpellBook spellBook;
	//

	//
	private Vector3 min, max;

	[SerializeField]
	private Transform[] staffGems;
	//
	private int gemIndex;
	//
	[SerializeField]
	private SightBlock[] sightblocks;

	//
	public Transform Target {
		get;
		set;
	}


	// Use this for initialization (Before Start)
	protected override void Awake () {
		
		spellBook = GetComponent <SpellBook> ();
		mana.Initialize (initManaValue, initManaValue);

		base.Awake ();
	}


	// Update is called once per frame
	protected override void Update () {
		GetInput ();

		//
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, min.x, max.x), Mathf.Clamp (transform.position.y, min.y, max.y), transform.position.z );

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

		if (Input.GetKeyDown(KeyCode.I)) {
			health.MyCurrentValue += 10;
			mana.MyCurrentValue += 20;
		} 
		if (Input.GetKeyDown(KeyCode.O)) {
			health.MyCurrentValue -= 10;
			mana.MyCurrentValue -= 20;
		}
	}


	//
	public void SetMoveLimits (Vector3 min, Vector3 max) {
		//
		this.min = min;
		this.max = max;
	}


	// Test function to Attack
	private IEnumerator Attack (int spellIndex) {

		//
		Transform currentTarget = Target;
		// 
		Spell spell = spellBook.CastSpell (spellIndex);
		//
		isAttacking = true;
		//
		anim.SetBool ("Attack", isAttacking);

		yield return new WaitForSeconds (spell.GetCastTime);

		if (currentTarget != null && InLineOfSight () ) {
			//
			SpellScript spellScript = Instantiate (spell.GetSpellPrefab, staffGems[gemIndex].position, Quaternion.identity).GetComponent<SpellScript> ();
			//
			spellScript.Initialize (currentTarget, spell.GetDmg);
		}
		//
		StopAttack ();

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
		
		if (Target != null) {
			
			Vector3 targetDirection = (Target.position - transform.position).normalized;

			RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDirection, Vector2.Distance(transform.position, Target.position), LayerMask.GetMask("SightBlock"));

			if (hit.collider == null) {
				return true;
			}
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


	public override void StopAttack () {

		spellBook.StopCasting ();

		base.StopAttack ();

	}
}