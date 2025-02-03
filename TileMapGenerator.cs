using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGenerator : MonoBehaviour
{
    public int birthLimit = 3;
    public int deathLimit = 2;
    public int Chance = 50;
    public Tilemap topMap, colliderMap;
    public TileBase topTile, bottomTile;
    public int height, width;
    public int iter;
    public string saveAs = "myMap";
    private int[,] myGrid;
    // Start is called before the first frame update
    void Start()
    {
        myGrid = new int[height, width];
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                myGrid[i, j] = (Random.Range(1,101) < Chance) ? 1 : 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Simulate(iter);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //saveMap();
        }
    }

    /*
    private void saveMap()
    {
        var mp = GameObject.Find("Grid");
        if (mp != null)
        {
            var savePath = "Assets/" + saveAs + ".prefab";
            if (PrefabUtility.SaveAsPrefabAsset(mp,savePath))
            {
                Debug.Log("saved");
            }
            else
            {
                Debug.Log("error in saving");
            }
        }
    }
    */
    private void Simulate(int numIter)
    {
        topMap.ClearAllTiles();
        colliderMap.ClearAllTiles();

        for( int i = 0; i < numIter; i++)
        {
            myGrid = updateTiles(myGrid);
        }
        myGrid = setBoundary(myGrid);
        for( int i = 0;i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (myGrid[i, j] == 0)
                {
                    colliderMap.SetTile(new Vector3Int(-i + width / 2, -j + height / 2, 0), bottomTile);
                    
                }
                topMap.SetTile(new Vector3Int(-i + width / 2, -j + height / 2, 0), topTile);
            }
        }
    }
    private int[,] updateTiles(int[,] oldGrid)
    {
        int[,] newGrid = new int[height, width];
        for(int i = 0;i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                Vector2Int pos = new Vector2Int(j, i);
                Vector2Int[] neighbours = getNeighbours(pos);
                int aliveCt = 0;
                foreach(Vector2Int neighbour in neighbours)
                {
                    if(neighbour.x < 0 || neighbour.y < 0 || neighbour.x >= width || neighbour.y >= height) continue;
                    if(oldGrid[neighbour.y, neighbour.x] ==1) aliveCt++;
                }
                if (oldGrid[i, j] == 1)
                {
                    if (aliveCt < deathLimit) newGrid[i, j] = 0;
                    else newGrid[i, j] = 1;
                }
                else
                {
                    if (aliveCt > birthLimit) newGrid[i, j] = 1;
                    else newGrid[i, j] = 0;
                }
            }
        }
        return newGrid;
    }

    private Vector2Int[] getNeighbours(Vector2Int pos)
    {
        Vector2Int[] neighbours = new Vector2Int[8];
        neighbours[0] = pos + new Vector2Int(1,0);
        neighbours[1] = pos + new Vector2Int(-1, 0);
        neighbours[2] = pos + new Vector2Int(0, 1);
        neighbours[3] = pos + new Vector2Int(0, -1);
        neighbours[4] = pos + new Vector2Int(1, 1);
        neighbours[5] = pos + new Vector2Int(-1, 1);
        neighbours[6] = pos + new Vector2Int(1, -1);
        neighbours[7] = pos + new Vector2Int(-1, -1);
        return neighbours;
    }

    private int[,] setBoundary(int[,] grid)
    {
        int[,] boundedGrid = new int[height, width];
        for(int i= 0; i<height; i++) { 
            for(int j = 0; j<width; j++)
            {
                if (i == 0 || j==0 || i==height-1 || j==width-1)
                {
                    boundedGrid[i,j] = 0;
                }
                else
                {
                    boundedGrid[i,j] = grid[i,j];
                }
            }
        }
        return boundedGrid;
    }
}
