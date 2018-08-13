using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// I usable.
/// 
/// </summary>

	//
public interface IUseable {
	//
	Sprite GetIcon { get; }
	//
	void Use ();
}