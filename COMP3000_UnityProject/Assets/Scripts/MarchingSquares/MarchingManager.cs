using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarchingManager : MonoBehaviour
{
	public List<TileBase> Tiles { get; set; }
	public Tilemap tilemap { get; set; }

	public BoundsInt tilemapBounds;

	public float[,] verticesArray;

	private void Start()
	{
		tilemap.CompressBounds();
		tilemapBounds = tilemap.cellBounds;

		generateVerticesArray();
	}

	private void generateVerticesArray()
	{
		// use the tilemap and the tilemap bounds to generate 2darray of vertices
	}

}
