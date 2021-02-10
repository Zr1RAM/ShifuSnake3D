// Class that handles all capabilities of the map.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    GameObject TilePrefab;     //The prefab used for the tile.
    GameObject[,] TileSystem; // 2D array of GameObject that holds all the tiles. This is believed to be a bit of a brute force method. 
                             // Was looking into using particle system for this but that might take more time so reverted to the traditional approach

    // MapController Startup
    public void InitializeMapController(int width,int height)
    {
        transform.localScale = new Vector3(width,this.transform.localScale.y,height);
        transform.position = new Vector3((float)width/2, 0 ,(float)height/2); // Setting position with offset from origin so that the maze fits in the camera.
        InitializeTilesSystem(width, height);
    }
    // These variables were made visible in Editor for debugging purposes.
    [SerializeField]
    List<TileCoordinate> AvailableCoordinates; // List of tiles that on spawn or not traversed by snake.
    [SerializeField]
    List<TileCoordinate> UsedCoordinates; // List of tiles that are traversed by snake.

    // Spawning tiles.
    void InitializeTilesSystem(int Width,int Height)
    {
        TileSystem = new GameObject[Width, Height];
        AvailableCoordinates = new List<TileCoordinate>();
        UsedCoordinates = new List<TileCoordinate>();
        GameObject SpawnedTile;
        Vector3 TileSpawnLocation;
        for (int i =0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                TileSpawnLocation = new Vector3(0.5f + i, -0.018f, 0.5f + j);
                SpawnedTile = Instantiate(TilePrefab, TileSpawnLocation, Quaternion.identity);
                SpawnedTile.name = i.ToString() + "," + j.ToString(); // Setting Spawned Tile GameObject Name as the Index i,j. Snake's OnCollisionEnter will provide the GameObject Name for us to obtain "i,j".
                SpawnedTile.transform.SetParent(transform,true); 
                AvailableCoordinates.Add(new TileCoordinate(i, j));
                TileSystem[i, j] = SpawnedTile;
            }
        }
    }
    // Disable Tile with provided "i,j" index string.
    // AvailableCoordinates item of index is removed and added to UsedCoordinates
    // When AvailableCoordinates 0 it means all tiles are traversed and GameOver.
    public void DisableTileAt(string TileName)
    {
        string[] CurrentTileName = TileName.Split(',');
        int i = Int32.Parse(CurrentTileName[0]);
        int j = Int32.Parse(CurrentTileName[1]);
        TileSystem[i, j].SetActive(false);
        UsedCoordinates.Add(new TileCoordinate(i, j));
        //print("used coordinates count: " + UsedCoordinates.Count);
        AvailableCoordinates.RemoveAll((Coordinate) => Coordinate.x == i && Coordinate.y == j); // Remove TileCoordinate of value i,j from list.
        if(AvailableCoordinates.Count == 0)
        {
            print("gamedone");
            GameManager.gameManagerInstance.GameOver(); // 
        }
    }
    // Function to reset tiles
    // Original idea is to have UsedCoordinates reset the tiles and get added back to AvailableCoordinates. Faced some issues.
    // Hence for good measure..
    public void ResetTiles()
    {
        //for(int i = 0; i < UsedCoordinates.Count; i++)
        //{
        //    TileSystem[UsedCoordinates[i].x, UsedCoordinates[i].y].SetActive(true);
        //    AvailableCoordinates.Add(UsedCoordinates[i]);
        //    UsedCoordinates.RemoveAt(i);
        //}
        for(int i = 0; i < UsedCoordinates.Count; i++)
        {
            TileSystem[UsedCoordinates[i].x, UsedCoordinates[i].y].SetActive(true);
        }
        for (int j = 0; j < AvailableCoordinates.Count; j++)
        {
            TileSystem[AvailableCoordinates[j].x, AvailableCoordinates[j].y].SetActive(true);
        }
        AvailableCoordinates.AddRange(UsedCoordinates);
        UsedCoordinates.Clear();
    }
    // Provides random position from list of tiles in AvailableCoordinates.
    public Vector3 AvailableTilePosition()
    {
        int RandomIndex = UnityEngine.Random.Range(0, AvailableCoordinates.Count);
        return TileSystem[AvailableCoordinates[RandomIndex].x, AvailableCoordinates[RandomIndex].y].transform.position;
    }
    [System.Serializable]
    class TileCoordinate
    {
        public int x, y;
        public TileCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
