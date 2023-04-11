using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
	public List<Square> Squares { get; set; }
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

}
