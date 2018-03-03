/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// Level director.
/// 
/// </summary>

public class LevelDirector : MonoBehaviour {
	//
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
	private Dictionary <Point,GameObject> waterTiles = new Dictionary <Point, GameObject> ();
	//
	[SerializeField] private SpriteAtlas waterAtlas;


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
						if (newElement.GetTileTag == "Water") {
							//
							waterTiles.Add (new Point (x, y), obj);
						}

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
		CheckWater ();
	}


	//
	private void CheckWater () {
		//
		foreach (KeyValuePair<Point, GameObject> tile in waterTiles) {
			//
			string composition = TileCheck (tile.Key);

			// Water Corner
			if (composition[1] == 'E' && composition[3] == 'W' && composition[4] == 'E' && composition[6] == 'W' ) {
				//
				tile.Value.GetComponent<SpriteRenderer>().sprite = waterAtlas.GetSprite("T_WaterEdgeC");
			}

			// Water Corner Overlaps
			if (composition[1] == 'W' && composition[2] == 'E' && composition[4] == 'W') {
				//
				GameObject obj = Instantiate (tile.Value, tile.Value.transform.position, Quaternion.identity, map);
				SpriteRenderer objRenderer = obj.GetComponent <SpriteRenderer> ();
				objRenderer.sprite = waterAtlas.GetSprite ("T_WaterEdgeWoodC");
				objRenderer.sortingOrder = 1;
			}

			// For Overlapping WaterFX (waves)
			if (composition[1] == 'W' && composition[2] == 'W' && composition[4] == 'W' && composition[6] == 'W') {
				//
				int randomChance = UnityEngine.Random.Range (0,100);
				if (randomChance < 15) {
					//
					tile.Value.GetComponent<SpriteRenderer> ().sprite = waterAtlas.GetSprite("T_WaterMid");
				}
			}
			// Place Tile (lilypads) Surrounded by Water
			if (composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W') {
				//
				int randomChance = UnityEngine.Random.Range (0,100);
				if (randomChance < 10) {
					//
					tile.Value.GetComponent<SpriteRenderer> ().sprite = waterAtlas.GetSprite("T_WaterMid");
				}
			}
		}

	}


	//
	private string TileCheck (Point currentPoint) {
		//
		string composition = string.Empty;

		//
		for (int x = -1; x <= 1; x++) {
			//
			for (int y = -1; y <= 1; y++) {
				//
				if (x != 0 || y != 0) {
					//
					if (waterTiles.ContainsKey (new Point (currentPoint.MyX + x, currentPoint.MyY + y))) {
						//
						composition += "W";
					} else {
						//
						composition += "E";
					}
				}
			}
		}
		//
		return composition;
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


/// <summary>
/// Point.
/// 
/// </summary>
public struct Point {
	//
	public int MyX { get; set; }
	public int MyY { get; set; }

	//
	public Point (int x, int y) {
		//
		this.MyX = x;
		this.MyY = y;
	}
}
*/