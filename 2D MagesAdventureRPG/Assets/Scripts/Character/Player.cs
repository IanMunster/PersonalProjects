using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// 
/// </summary>

public class Player : Character {

	// Singleton Structure
	private static Player instance;
	//
	public static Player GetInstance {
		get {
			if (instance == null) {
				instance = FindObjectOfType <Player> ();
			}
			return instance;
		}
	}

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


	// Use this for initialization (Before Start)
	protected override void Start () {
		
		spellBook = GetComponent <SpellBook> ();
		mana.Initialize (initManaValue, initManaValue);

		base.Start ();
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

		//
		Direction = Vector2.zero;

		if (Input.GetKey (KeybindDirector.GetInstance.Keybinds["UP"])) {
			gemIndex = 0;
			Direction += Vector2.up;
		}
		if (Input.GetKey (KeybindDirector.GetInstance.Keybinds["LEFT"])) {
			gemIndex = 1;
			Direction += Vector2.left;
		}
		if (Input.GetKey (KeybindDirector.GetInstance.Keybinds["DOWN"])) {
			gemIndex = 3;
			Direction += Vector2.down;
		}
		if (Input.GetKey (KeybindDirector.GetInstance.Keybinds["RIGHT"])) {
			gemIndex = 2;
			Direction += Vector2.right;
		}


		//
		if (IsMoving) {
			StopAttack ();
		}

		//
		foreach (string action in KeybindDirector.GetInstance.Actionbinds.Keys) {
			// 
			if (Input.GetKeyDown (KeybindDirector.GetInstance.Actionbinds[action])) {
				//
				UIDirector.GetInstance.ClickActionButton (action);
			}
		}
	}


	//
	public void SetMoveLimits (Vector3 min, Vector3 max) {
		//
		this.min = min;
		this.max = max;
	}


	// Test function to Attack
	private IEnumerator Attack (string spellName) {

		//
		Transform currentTarget = Target;
		// 
		Spell spell = spellBook.CastSpell (spellName);
		//
		IsAttacking = true;
		//
		Anim.SetBool ("Attack", IsAttacking);

		yield return new WaitForSeconds (spell.GetCastTime);

		if (currentTarget != null && InLineOfSight () ) {
			//
			SpellScript spellScript = Instantiate (spell.GetSpellPrefab, staffGems[gemIndex].position, Quaternion.identity).GetComponent<SpellScript> ();
			//
			spellScript.Initialize (currentTarget, spell.GetDmg, transform);
		}
		//
		StopAttack ();

	}


	//
	public void CastSpell (string spellName) {
		//
		BlockSight ();
		//
		if (Target != null && Target.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight()) {
			//
			attackRoutine = StartCoroutine (Attack(spellName));
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


	//
	public void StopAttack () {
		spellBook.StopCasting ();

		if (attackRoutine != null) {
			StopCoroutine (attackRoutine);
			//
			IsAttacking = false;
			//
			Anim.SetBool ("Attack", IsAttacking);
		}
	}
}