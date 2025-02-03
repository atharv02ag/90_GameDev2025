using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;


public class Pickaxe : MonoBehaviour
{
    public Tilemap tilemap; // Reference to the tilemap
    public float range = 1.5f; // Distance to destroy tiles
    public int durability = 10;
    public TileBase reward;
    private Camera mainCamera;
    private LogicScript logic;

    void Start()
    {
        mainCamera = Camera.main;
        logic = GameObject.FindGameObjectWithTag("logic").gameObject.GetComponent<LogicScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            DestroyTile();
        }
    }

    void DestroyTile()
    {
        // Get the direction the pickaxe or player is facing
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);

        // Get the target tile position
        Vector3 pos = transform.position - cellPosition;

        // Check if there’s a tile at the target position
        if (tilemap.HasTile(cellPosition) && pos.magnitude < range && durability>0)
        {
            if (tilemap.GetTile(cellPosition) == reward) {
                logic.changeScore(1);
            }
            durability--;
            tilemap.SetTile(cellPosition, null); // Destroy the tile
        }
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}

