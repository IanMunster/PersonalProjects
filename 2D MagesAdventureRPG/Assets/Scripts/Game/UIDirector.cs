using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDirector : MonoBehaviour {

	// Singleton Structure
	private static UIDirector instance;
	//
	public static UIDirector GetInstance {
		get {
			if (instance == null) {
				instance = FindObjectOfType <UIDirector> ();
			}
			return instance;
		}
	}


	//
	[SerializeField]
	private Button[] actionButtons;
	//
	private KeyCode action1, action2, action3;

	//
	[SerializeField]
	private GameObject targetFrame;
	//
	private Stat targetFrameHealth;
	//
	private Image targetFramePortrait;


	// Use this for initialization
	void Start () {
		// key binds
		action1 = KeyCode.Alpha1;
		action2 = KeyCode.Alpha2;
		action3 = KeyCode.Alpha3;

		//
		targetFrameHealth = targetFrame.GetComponentInChildren<Stat> ();
		//
		targetFramePortrait = targetFrame.transform.GetChild(0).GetChild(1).GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(action1)) {
			ActionButtonOnClick (0);
		}

		if (Input.GetKeyDown(action2)) {
			ActionButtonOnClick (1);
		}

		if (Input.GetKeyDown(action3)) {
			ActionButtonOnClick (2);
		}
	}


	//
	private void ActionButtonOnClick (int buttonIndex) {
		// 
		actionButtons[buttonIndex].onClick.Invoke();

	}


	//
	public void ShowTargetFrame (NPC target) {
		//
		targetFrame.SetActive (true);
		//
		targetFrameHealth.Initialize (target.GetHealthStat.MyCurrentValue, target.GetHealthStat.MyMaxValue);
		//
		targetFramePortrait.sprite = target.GetPortrait;
		//
		target.healthChanged += new HealthChanged (UpdateTargetFrame);
		//
		target.characterRemoved += new CharacterRemoved (HideTargetFrame);
	}

	//
	public void HideTargetFrame () {
		//
		targetFrame.SetActive (false);
	}


	//
	public void UpdateTargetFrame (float health) {
		//
		targetFrameHealth.MyCurrentValue = health;
	}
}