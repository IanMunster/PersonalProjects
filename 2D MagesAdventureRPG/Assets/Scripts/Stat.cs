using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stat.
/// 
/// </summary>

public class Stat : MonoBehaviour {


	// Slider of the Stat (UI)
	private Slider slider;
	// Text on UI for value
	private Text valueText;
	// Value of the Stat
	private float currentValue;
	// Max value of Stat
	private float maxValue;
	// Value to smoothen the Adding and Subtrackting to Stat
	private float statUISmooth  = 1f;

	// Get and Set MaxFillValue of Slider
	public float MyMaxValue {
		get {
			return maxValue;
		}
		set {
			maxValue = value;
		}
	}


	// Get and Set Value of the Stat
	public float MyCurrentValue {
		get {
			return currentValue;
		}
		set {
			// Check if Health tries to go above Max
			if (value > maxValue) {
				// Set to Max
				currentValue = maxValue;
			// Check if Less then 0
			} else if (value < 0) {
				//Set to Zero
				currentValue = 0;
			} else {
				currentValue = value;
			}
		}
	}


	// Use this for initialization
	void Start () {
		// Get Components
		slider = GetComponent <Slider> ();
		valueText = GetComponentInChildren <Text> ();
	}


	// Update is called once per frame
	void Update () {
		// Set UI values of Sliders to Correct Value
		if (currentValue != slider.value) {
			// Do Nice transition
			slider.value = Mathf.Lerp (slider.value, currentValue, statUISmooth * Time.deltaTime);
		} else {
			// Just ajust value
			slider.value = currentValue;
		}
		// Set UI TextValue
		valueText.text = currentValue + " / " + maxValue;

		// Check if Max has Changed
		if (maxValue != slider.maxValue) {
			slider.maxValue = maxValue;
		}
	}


	// Initialize Game Start Stats
	public void Initialize (float current, float max) {
		// Set max Value to Init Max
		MyMaxValue = max;
		// Set current Value to Init Current
		MyCurrentValue = current;
	}
}