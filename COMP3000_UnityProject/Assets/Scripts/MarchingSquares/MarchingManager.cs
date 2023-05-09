using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class MarchingManager : MonoBehaviour
{
	[SerializeField]
	public List<TileBase> Tiles;

	public Tilemap tilemap;

	[SerializeField]
	public BoundsInt tilemapBounds;

	[SerializeField]
	public Vertex[,] verticesArray;
	[SerializeField]
	public Square[,] squaresArray;

	[SerializeField]
	public int tilemapXSize;
	[SerializeField]
	public int tilemapYSize;

	public GameObject player;
	public GameObject mapMarker;
	public Vector3 playerPosition;
	public Collider2D forestCollider;
	public Bounds forestBounds;

	[SerializeField]
	public int forestXSize;
	[SerializeField]
	public int forestYSize;

	public float translationMultiplier;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		generateSquaresArray();
		generateVerticesArray();

		calculateForestToTilemapMultiplier();
	}

	private void Update()
	{
		// get player position in map
		playerPosition = player.transform.position;

		//transalte to vertex in tilemap
		Vector3Int vertexPos = translatePlayerPosToTilemap(playerPosition);

		// turn vertex on
		turnVertexOn(vertexPos);
	}

	private void turnVertexOn(Vector3Int currentVertexPos)
	{
		//get vertex from array
		Vertex currentVertex = verticesArray[currentVertexPos.x, currentVertexPos.y];

		if(!currentVertex.IsOn)
		{
			currentVertex.IsOn = true;
		}
		
	}

	private Vector3Int translatePlayerPosToTilemap(Vector3 playerWorldPosition)
	{
		//returns position of current vertex pos in tilemap

		Vector3Int playerTilemapPosition;


		if (forestXSize/forestYSize != tilemapXSize/tilemapYSize)
		{
			Debug.Log("ERROR: the tilemap and forest have different ratios.");
			playerTilemapPosition = new Vector3Int(0, 0, 0);
		}
		else
		{
			//tilemap and forest have the same ratios, as required. 

			//multiply playerWorldPos in forest by multiplier to get equivalent vertex on tilemap

			float newPositionX = playerWorldPosition.x / translationMultiplier;
			float newPositionY = playerWorldPosition.y / translationMultiplier;

			playerTilemapPosition = new Vector3Int(Mathf.RoundToInt(newPositionX), Mathf.RoundToInt(newPositionY), 0);
			mapMarker.transform.localPosition = new Vector3(newPositionX, newPositionY, 0);
		}
		// tilemapBounds bounds of tilemap

		//Debug.Log("Player world pos is " + playerWorldPosition + " and tilemap pos is " + playerTilemapPosition);
		return playerTilemapPosition;
		
	}

	private void calculateForestToTilemapMultiplier()
	{
		// takes in the forest and tilemap info and returns the number to multiply one with to get the other
		// forestPos * multiplier = tilemapPos

		// forestBounds size/bounds of the world space forest
		forestBounds = forestCollider.bounds;
		forestXSize = Mathf.RoundToInt(forestBounds.size.x);
		forestYSize = Mathf.RoundToInt(forestBounds.size.y);


		translationMultiplier = (forestXSize / tilemapXSize);

		if(translationMultiplier != (forestYSize/ tilemapYSize)) 
		{
			//somethings squiffy - not same ratio
			Debug.Log("ERROR identifying translation multiplier. Tilemap and Forest space have different ratios.");
		}
	}


	private void generateSquaresArray()
	{
		// use the tilemap and bounds to get 2darray of squares
		tilemap.CompressBounds();
		tilemapBounds = tilemap.cellBounds;



		//create the squares array using the tilemap bounds info to set size
		//get x size
		tilemapXSize = tilemapBounds.size.x;
		tilemapYSize = tilemapBounds.size.y;

		squaresArray = new Square[tilemapXSize, tilemapYSize];

		for (int x = 0; x < tilemapXSize; x++)
		{
			for (int y = 0; y < tilemapYSize; y++)
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

		verticesArray = new Vertex[tilemapXSize + 1, tilemapYSize + 1];
		
		//iterate through each square, create vertices, add to vertices array if not there, assign to square

		for (int x = 0; x < tilemapXSize; x++) 
		{
			for (int y = 0; y < tilemapYSize; y++)
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
