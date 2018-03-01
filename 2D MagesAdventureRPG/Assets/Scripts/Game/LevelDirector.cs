using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Level director.
/// 
/// </summary>

public class LevelDirector : MonoBehaviour {

	[SerializeField]
	private Transform map;
	//
	[SerializeField]
	private Texture2D[] mapData;
	//
	[SerializeField]
	private MapElement[] mapElements;
	//
	[SerializeField]
	private Sprite defaultTile;
	//
	//private Dictionary <,>

	//
	private Vector3 GetWorldStartPos {
		//
		get { return Camera.main.ScreenToWorldPoint (new Vector3(0,0)); }
	}

	// Use this for initialization
	void Start () {
		GenerateMap ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//
	private void GenerateMap () {

		//
		int height = mapData [0].height;
		int width = mapData [0].width;

		//
		for (int i = 0; i < mapData.Length; i++) {
			//
			for (int x = 0; x < mapData[i].width; x++) {
				for (int y = 0; y < mapData[i].height; y++) {
					//
					Color pixelColor = mapData[i].GetPixel(x,y);

					//
					MapElement newElement = Array.Find (mapElements, check => check.GetTileColor == pixelColor);
					//
					if (newElement != null) {
						//
						float xPos = GetWorldStartPos.x + (defaultTile.bounds.size.x * x);
						float yPos = GetWorldStartPos.y + (defaultTile.bounds.size.y * y);
						//
						GameObject obj = Instantiate(newElement.GetElementPrefab);
						//
						obj.transform.position = new Vector2 (xPos, yPos);
						//
						if (newElement.GetTileTag == "Tree" ) {
							//
							obj.GetComponent<SpriteRenderer> ().sortingOrder = height * 2 - y * 2;
						}
						//
						obj.transform.parent = map;
					}
				}
			}
		}
	}
}


/// <summary>
/// Map element.
/// 
/// </summary>
[Serializable]
public class MapElement {
	//
	[SerializeField]
	private string tileTag;
	//
	[SerializeField]
	private Color tileColor;
	//
	[SerializeField]
	private GameObject elementPrefab;

	//
	public string GetTileTag {
		get { return tileTag; }
	}
	//
	public Color GetTileColor {
		get { return tileColor; }
	}
	//
	public GameObject GetElementPrefab {
		get { return elementPrefab; }
	}
}