using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Square : MonoBehaviour
{
    public List<Vertex> Vertices { get; set; }
    public List<int> conversion = new List<int> { 8, 4, 2, 1 };
    public MarchingManager marchingManager;
    public Vector3Int squareTilePosition;

    public int State { get; set; }

    public Square(Vector3Int squarePosition, MarchingManager marchingMan)
    {
        squareTilePosition= squarePosition;
        marchingManager= marchingMan;
    }

    public void OnVertexUpdated()
    {
        // State = make vertex map to number from 0-15 

        int MarchingSquareRef = 0;

        for(int i = 0; i >= Vertices.Count; i++)
        {
            if (Vertices[i].IsOn)
            {
				MarchingSquareRef += conversion[i];
			}
		}

        //update the sprite on the tile
        updateTileSprite(MarchingSquareRef);
    }

    public void updateTileSprite(int newMarchingTileRef)
    {
        //Takes the number of the new marching sqaure image to be used, updates the tile sprite 
        TileBase newTile = marchingManager.Tiles[newMarchingTileRef];
        marchingManager.tilemap.SetTile(squareTilePosition, newTile);
    }    
}
