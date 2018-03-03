using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

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


	public Spell CastSpell (int spellIndex) {
		
		actionTimer.color = spells [spellIndex].GetTimerColor;

		actionName.text = spells [spellIndex].GetName;

		actionIcon.sprite = spells [spellIndex].GetIcon;

		spellRoutine = StartCoroutine (Progress (spellIndex));

		fadeRoutine = StartCoroutine (FadeTimer ());

		return spells [spellIndex];
	}


	//
	private IEnumerator Progress (int spellIndex) {
		// Start time
		float timePassed = Time.deltaTime;
		// Rate of decrease based on CastingTime
		float rate =  1.0f / spells [spellIndex].GetCastTime;
		//
		float progress = 0.0f;

		//
		while (progress <= 1f) {
			//
			actionTimer.fillAmount = Mathf.Lerp (0, 1, progress);
			//
			progress += rate * Time.deltaTime;

			timePassed += Time.deltaTime;

			actionTime.text = (spells [spellIndex].GetCastTime - timePassed).ToString ("F1");

			if (spells[spellIndex].GetCastTime - timePassed < 0 || progress < 0) {
				actionTime.text = "0.0";
				actionTimer.fillAmount = 1;
			}
			//
			yield return null;
		}

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

}
