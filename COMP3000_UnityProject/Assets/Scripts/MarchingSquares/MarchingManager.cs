using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarchingManager : MonoBehaviour
{
	public List<TileBase> Tiles { get; set; }
	public Tilemap tilemap;

	public BoundsInt tilemapBounds;

	public float[,] verticesArray;
	public Square[,] squaresArray;

	private void Start()
	{
		generateSquaresArray();
		generateVerticesArray();
	}

	private void generateSquaresArray()
	{
		// use the tilemap and bounds to get 2darray of squares
		tilemap.CompressBounds();
		tilemapBounds = tilemap.cellBounds;

		Debug.Log("Bounds are " + tilemapBounds);

		//create the squares array using the tilemap bounds info to set size
		//get x size
		int xSize = tilemapBounds.size.x;
		int ySize = tilemapBounds.size.y;

		squaresArray = new Square[xSize, ySize];

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				// iterate through every tile within tilemap bounds

				//get the position of current square
				var px = tilemapBounds.xMin + x;
				var py = tilemapBounds.yMin + y;
				Vector3Int currentPos = new Vector3Int(px, py, 0);

				if (tilemap.HasTile(currentPos))
				{
					//create new square
					Square newSquare = new Square(currentPos, this);

					//add to 2darray in size pos
					squaresArray[x, y] = newSquare;
					Debug.Log("square at position " + currentPos + " in world was added to position " + x + " , " + y + " in squares array");
				}
				else
				{
					Debug.Log("no tile at pos");
				}
			}
		}
	}
	private void generateVerticesArray()
	{
		// use squares array to generate vertices array
	}

}
