﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField]
    GameObject TilePrefab;
    GameObject[,] TileSystem;
    // Start is called before the first frame update
    public void InitializeMapController(int width,int height)
    {
        transform.localScale = new Vector3(width,this.transform.localScale.y,height);
        transform.position = new Vector3((float)width/2, 0 ,(float)height/2);
        InitializeTilesSystem(width, height);
    }
    [SerializeField]
    List<TileCoordinate> AvailableCoordinates;
    [SerializeField]
    List<TileCoordinate> UsedCoordinates;
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
                SpawnedTile.name = i.ToString() + "," + j.ToString();
                SpawnedTile.transform.SetParent(transform,true); 
                AvailableCoordinates.Add(new TileCoordinate(i, j));
                TileSystem[i, j] = SpawnedTile;
            }
        }
    }
    
    public void DisableTileAt(string TileName)
    {
        string[] CurrentTileName = TileName.Split(',');
        int i = Int32.Parse(CurrentTileName[0]);
        int j = Int32.Parse(CurrentTileName[1]);
        TileSystem[i, j].SetActive(false);
        UsedCoordinates.Add(new TileCoordinate(i, j));
        //print("used coordinates count: " + UsedCoordinates.Count);
        AvailableCoordinates.RemoveAll((Coordinate) => Coordinate.x == i && Coordinate.y == j);
        if(AvailableCoordinates.Count == 0)
        {
            print("gamedone");
            GameManager.gameManagerInstance.GameOver();
        }
    }
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
