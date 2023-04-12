using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarchingManager : MonoBehaviour
{
	[SerializeField]
	public List<TileBase> Tiles { get; set; }
	public Tilemap tilemap;

	[SerializeField]
	public BoundsInt tilemapBounds;

	[SerializeField]
	public Vertex[,] verticesArray;
	[SerializeField]
	public Square[,] squaresArray;

	[SerializeField]
	public int xSize;
	[SerializeField]
	public int ySize;

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
		xSize = tilemapBounds.size.x;
		ySize = tilemapBounds.size.y;

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

		// vertices array is always one bigger than the squares array
		// bc each square has 4 vertices

		verticesArray = new Vertex[xSize + 1, ySize + 1];
		Debug.Log("new vertices array created with size " + (xSize + 1) + " : " + (ySize + 1));
		//iterate through each square, create vertices, add to vertices array if not there, assign to square

		for (int x = 0; x < xSize; x++) 
		{
			for (int y = 0; y < ySize; y++)
			{
				Square currentSquare = squaresArray[x, y];

				for (int i = 0; i < 4; i++)
				{
					//generate current vertex from square pos and iteration
					
					int xOffset = i % 2;
					int yoffset = 0;

					Vector3Int vertexPosition = new Vector3Int(x+xOffset, y+yoffset, 0);

					if(i >= 2)
					{
						yoffset = 1;
					}

					Vertex currentVertex = verticesArray[x + xOffset, y + yoffset];

					if (currentVertex == null)
					{
						currentVertex = new Vertex(vertexPosition);
						verticesArray[x + xOffset, y + yoffset] = currentVertex;
					}

					//add vertex to the current squares vertex list
					currentSquare.Vertices.Add(currentVertex);

					//add the square to the vertex's square list
					currentVertex.Squares.Add(currentSquare);

				}
						
			}
		}

	}

}
