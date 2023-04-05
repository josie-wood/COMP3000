using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
	private bool isOn;
	public bool IsOn { 
		get 
		{
			return isOn;
		} 
		set
		{
			// sets isOn() and updates squares accordingly
			isOn = value;
			foreach(Square square in Squares)
			{
				square.OnVertexUpdated();
			}
		}
	}

	public List<Square> Squares { get; set; }
}
