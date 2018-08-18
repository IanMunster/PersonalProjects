using System;
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
	private ActionButton[] actionButtons;

	//
	[SerializeField]
	private GameObject targetFrame;
	//
	private Stat targetFrameHealth;
	//
	private Image targetFramePortrait;

	//
	[SerializeField]
	private CanvasGroup keybindMenu;
	//
	private GameObject[] keybindButtons;

	//
	private void Awake () {
		//
		keybindButtons = GameObject.FindGameObjectsWithTag("KeyBind");
	}

	// Use this for initialization
	void Start () {
		//
		targetFrameHealth = targetFrame.GetComponentInChildren<Stat> ();
		//
		targetFramePortrait = targetFrame.transform.GetChild(0).GetChild(1).GetComponent<Image>();

		//
		SetUseable (actionButtons[0], SpellBook.GetInstance.GetSpell("FireBolt") );
		SetUseable (actionButtons[1], SpellBook.GetInstance.GetSpell("FrostBolt") );
		SetUseable (actionButtons[2], SpellBook.GetInstance.GetSpell("ShockBolt") );
	}
	
	// Update is called once per frame
	void Update () {
		//
		if (Input.GetKeyDown(KeyCode.Escape)) {
			//
			OpenCloseMenu ();
		}
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

	//
	public void OpenCloseMenu (){
		//
		keybindMenu.alpha = keybindMenu.alpha > 0 ? 0 : 1;
		//
		keybindMenu.blocksRaycasts = keybindMenu.blocksRaycasts == true ? false : true;
		//
		Time.timeScale = Time.timeScale > 0 ? 0 : 1;
	}


	//
	public void UpdateKeyText(string key, KeyCode code) {
		//
		Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
		//
		tmp.text = code.ToString();
	}


	// 
	public void ClickActionButton (string buttonName) {
		//
		Array.Find (actionButtons, x => x.gameObject.name == buttonName).GetButton.onClick.Invoke();
	}


	//
	public void SetUseable (ActionButton button, IUseable useable) {
		// 
//		button.GetButton.image.sprite = useable.GetIcon;
		button.GetButton.image.color = Color.white;
		button.GetUseable = useable;
	}
}