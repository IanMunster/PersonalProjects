using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spell Script.
/// 
/// </summary>

public class SpellScript : MonoBehaviour {

	//
	private Rigidbody2D rigid;
	//
	private Animator anim;
	//
	[SerializeField]
	private float speed;
	//
	private int damage;

	//
	public Transform Target {
		get;
		private set;
	}




	// Use this for initialization
	void Start () {
		//
		rigid = GetComponent <Rigidbody2D> ();
		anim = GetComponentInChildren <Animator> ();
	}

	public void Initialize (Transform target, int damage) {
		this.Target = target;
		this.damage = damage;
	}

	// Update is called once per frame
	void Update () {
		
	}


	// Every Physics Step
	void FixedUpdate () {
		if (Target != null) {
			anim.SetTrigger ("Move");
			// Direction towards Target
			Vector2 direction = Target.position - transform.position;
			//
			rigid.velocity = direction.normalized * speed;
			// Angle in degrees
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			//
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		} 
	}

	//
	private void OnTriggerEnter2D (Collider2D collision) {

		//
		if (collision.tag == "HitBox" && collision.transform == Target) {
			//
			speed = 0;
			//
			collision.GetComponentInParent <Enemy>().TakeDamage (damage);
			//
			anim.SetTrigger ("Hit");
			//
			rigid.velocity = Vector2.zero;
			//
			Target = null;
		}
	}
}
