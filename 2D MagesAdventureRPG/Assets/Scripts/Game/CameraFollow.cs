using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Camera follow.
/// 
/// </summary>

public class CameraFollow : MonoBehaviour {

	//
	private Transform target;
	private Player player;
	//
	private Vector2 min, max;
	//
	[SerializeField]
	private Tilemap tilemap;


	// Use this for initialization
	void Start () {
		//
		target = GameObject.FindGameObjectWithTag("Player").transform;
		player = target.GetComponent<Player> ();
		//
		Vector3 minTile = tilemap.CellToWorld (tilemap.cellBounds.min);
		Vector3 maxTile = tilemap.CellToWorld (tilemap.cellBounds.max);
		//
		SetLimits (minTile, maxTile);
		//
		player.SetMoveLimits (minTile, maxTile);
	}

	// After physics
	private void LateUpdate () {
		//
		transform.position = new Vector3 ( Mathf.Clamp (target.position.x, min.x, max.x), Mathf.Clamp (target.position.y, min.y, max.y), transform.position.z );
	}


	//
	private void SetLimits (Vector3 minTile, Vector3 maxTile){
		//
		Camera cam = Camera.main;
		float height = cam.orthographicSize * 2f;
		float width = height * cam.aspect;
		//
		min.x = minTile.x + width / 2;
		max.x = maxTile.x - width / 2;
		//
		min.y = minTile.y + height / 2;
		max.y = maxTile.y - height / 2;
	}
}
