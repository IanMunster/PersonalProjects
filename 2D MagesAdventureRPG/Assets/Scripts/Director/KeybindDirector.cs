using System.Collections;
using System.Collections.Generic;
//
using System.Linq;
using UnityEngine;

/// <summary>
/// Keybind director.
/// </summary>

public class KeybindDirector : MonoBehaviour {


	//
	private static KeybindDirector instance;
	//
	public static KeybindDirector GetInstance {
		get {
			//
			if (instance == null) {
				instance = FindObjectOfType <KeybindDirector> ();
			}
			//
			return instance;
		}
	}


	//
	public Dictionary <string, KeyCode> Keybinds{ get; set; }
	//
	public Dictionary <string, KeyCode> Actionbinds { get; private set; }

	//
	private string bindName;

	// Use this for initialization
	void Start () {
		//
		Keybinds = new Dictionary <string, KeyCode> ();
		Actionbinds = new Dictionary <string, KeyCode> ();

		//
		BindKey("UP", KeyCode.W);
		BindKey("LEFT", KeyCode.A);
		BindKey("DOWN", KeyCode.S);
		BindKey("RIGHT", KeyCode.D);

		BindKey("ACT1", KeyCode.Alpha1);
		BindKey("ACT2", KeyCode.Alpha2);
		BindKey("ACT3", KeyCode.Alpha3);

	}


	//
	public void BindKey (string key, KeyCode keyBind) {
		//
		Dictionary<string, KeyCode> currentDictonary = Keybinds;
		//
		if (key.Contains ("ACT")) {
			//
			currentDictonary = Actionbinds;
		}

		//
		if (!currentDictonary.ContainsValue (keyBind)) {
			//
			currentDictonary.Add (key, keyBind);
			//
			UIDirector.GetInstance.UpdateKeyText(key, keyBind);
		//
		} else if (currentDictonary.ContainsValue (keyBind)) {
			//
			string myKey = currentDictonary.FirstOrDefault (x => x.Value == keyBind).Key;
			//
			currentDictonary[myKey] = KeyCode.None;
			//
			UIDirector.GetInstance.UpdateKeyText(key, KeyCode.None);
		}
		//
		currentDictonary [key] = keyBind;
		//
		UIDirector.GetInstance.UpdateKeyText(key, keyBind);
		//
		bindName = string.Empty;
	}
}
