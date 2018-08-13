using System;
using UnityEngine;

/// <summary>
/// Sight block.
/// 
/// </summary>

[Serializable]
public class SightBlock {

	//
	[SerializeField]
	private GameObject firstBlock, secondBlock;


	//
	public void Deactivate () {
		firstBlock.SetActive (false);
		secondBlock.SetActive (false);
	}


	//
	public void Activate () {
		firstBlock.SetActive (true);
		secondBlock.SetActive (true);
	}

}
