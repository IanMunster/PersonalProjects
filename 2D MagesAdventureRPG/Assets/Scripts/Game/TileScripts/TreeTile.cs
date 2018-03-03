using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

/// <summary>
/// Tree tile.
/// 
/// </summary>

public class TreeTile : Tile {

	public override bool StartUp (Vector3Int position, ITilemap tilemap, GameObject gameObject) {
		//
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = -position.y * 2;

		//
		return base.StartUp(position, tilemap, gameObject);
	}

	//
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Tiles/TreeTile")]
	public static void CreateWaterTile() {
		string path = EditorUtility.SaveFilePanelInProject("Save TreeTile", "New TreeTile", "asset", "Save TreeTile", "Assets");
		if (path == "") {
			return;
		}
		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TreeTile>(), path);
	}
	#endif
}
