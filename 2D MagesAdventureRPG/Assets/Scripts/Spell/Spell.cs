using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Spell.
/// 
/// </summary>

[Serializable]
public class Spell : IUseable {

	//
	[SerializeField]
	private string name;
	[SerializeField]
	private Sprite icon;
	[SerializeField]
	private GameObject spellPrefab;
	[SerializeField]
	private Color timerColor;

	//
	[SerializeField]
	private int dmg;
	[SerializeField]
	private float speed;
	[SerializeField]
	private float castTime;

	//
	public string GetName {
		get {
			return name;
		}
	}

	public Sprite GetIcon {
		get {
			return icon;
		}
	}

	public GameObject GetSpellPrefab {
		get {
			return spellPrefab;
		}
	}

	public Color GetTimerColor {
		get {
			return timerColor;
		}
	}

	//
	public int GetDmg {
		get {
			return dmg;
		}
	}

	public float GetSpeed {
		get {
			return speed;
		}
	}


	//
	public float GetCastTime {
		get {
			return castTime;
		}
	}


	// 
	public void Use() {
		//
		Player.GetInstance.CastSpell (GetName);
	}

}
