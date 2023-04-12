using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Vertex
{
	public List<Square> Squares { get; set; } = new List<Square>();
	private bool isOn;
	public Vector3Int VertexPosition;

	public Vertex(Vector3Int vertexPosition)
	{
		VertexPosition = vertexPosition;
	}

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
