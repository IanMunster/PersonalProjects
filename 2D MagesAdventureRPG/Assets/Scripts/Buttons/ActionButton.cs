using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Action button.
/// </summary>

public class ActionButton : MonoBehaviour, IPointerClickHandler {

	//
	private IUseable useable;
	//
	public Button GetButton { get; private set;}


	// Use this for initialization
	void Start () {
		//
		GetButton = GetComponent<Button>();
		//
		GetButton.onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick (){
		//
		if (useable != null) {
			//
			useable.Use ();
		}
	}

	public void OnPointerClick(PointerEventData eventData) {
		//
	}

}