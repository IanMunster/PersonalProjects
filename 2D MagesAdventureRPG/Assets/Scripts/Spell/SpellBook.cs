using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spell book.
/// </summary>

public class SpellBook : MonoBehaviour {

	// Singleton Structure
	private static SpellBook instance;
	//
	public static SpellBook GetInstance {
		get {
			if (instance == null) {
				instance = FindObjectOfType <SpellBook> ();
			}
			return instance;
		}
	}


	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private Spell[] spells;

	[SerializeField]
	private Image actionTimer;
	[SerializeField]
	private Text actionName;
	[SerializeField]
	private Image actionIcon;
	[SerializeField]
	private Text actionTime;

	private Coroutine spellRoutine;
	private Coroutine fadeRoutine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//
	public Spell CastSpell (string spellName) {
		//
		Spell spell = Array.Find (spells, x => x.GetName == spellName);
		//
		actionTimer.color = spell.GetTimerColor;
		//
		actionName.text = spell.GetName;
		//
		actionIcon.sprite = spell.GetIcon;
		//
		spellRoutine = StartCoroutine (Progress (spell));
		//
		fadeRoutine = StartCoroutine (FadeTimer ());
		//
		return spell;
	}


	//
	private IEnumerator Progress (Spell spell) {
		// Start time
		float timePassed = Time.deltaTime;
		// Rate of decrease based on CastingTime
		float rate =  1.0f / spell.GetCastTime;
		//
		float progress = 0.0f;

		//
		while (progress <= 1f) {
			//
			actionTimer.fillAmount = Mathf.Lerp (0, 1, progress);
			//
			progress += rate * Time.deltaTime;
			//
			timePassed += Time.deltaTime;
			//
			actionTime.text = (spell.GetCastTime - timePassed).ToString ("F1");
			//
			if (spell.GetCastTime - timePassed < 0 || progress < 0) {
				//
				actionTime.text = "0.0";
				//
				actionTimer.fillAmount = 1;
			}
			//
			yield return null;
		}
		//
		StopCasting ();
	}


	//
	public void StopCasting () {
		//
		if (fadeRoutine != null) {
			StopCoroutine (fadeRoutine);
			canvasGroup.alpha = 0;
			fadeRoutine = null;
		}

		if (spellRoutine != null) {
			StopCoroutine (spellRoutine);
			spellRoutine = null;
		}
	}


	//
	private IEnumerator FadeTimer () {
		// Rate of decrease based on CastingTime
		float rate =  1.0f / 0.5f;
		//
		float progress = 0.0f;

		//
		while (progress <= 1.01) {
			//
			canvasGroup.alpha = Mathf.Lerp (0, 1, progress);
			//
			progress += rate * Time.deltaTime;
			//
			yield return null;
		}
	}


	//
	public Spell GetSpell (string spellName){
		//
		Spell spell = Array.Find (spells, x => x.GetName == spellName);
		return spell;
	}
}
