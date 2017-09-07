using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;

        private Tile[,] tiles;

        // Use this for initialization
        void Start()
        {
            // Generate tiles on startup
            GenerateTiles();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Check is mouse is pressed
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Perform the raycast
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // Check if something was hit
                if (hit.collider != null)
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t)
                    {
                        int adjacentMines = GetAdjacentMineCountAt(t);
                        t.Reveal(adjacentMines);
                    }  
                }
            }


        }

        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = new Vector3(spacing / 2, spacing / 2, 0) + pos; // Position tile
            Tile currentTile = clone.GetComponent<Tile>(); // Get Tile component
            return currentTile; // Return it
        }

        void GenerateTiles()
        {
            // Create new 2D array of size width x height
            tiles = new Tile[width, height];

            // Loop through the entire list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Store half size for later use
                    Vector2 halfSize = new Vector2(width / 2, height / 2);
                    // Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);
                    // Apply spacing
                    pos *= spacing;
                    // Spawn the tile
                    Tile tile = SpawnTile(pos);
                    // Attach newly spawned tile to
                    tile.transform.SetParent(transform);
                    // Store its array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    // Store tile in array in those coordinates
                    tiles[x, y] = tile;

                }
            }
        }

        public int GetAdjacentMineCountAt(Tile t)
        {
            int count = 0;
            // Loop through for all elements and have each axis go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // Calculate desired coordinates from ones attained
                    int desiredX = t.x + x;
                    int desiredY = t.y + y;

                    if (desiredX < width && desiredX >= 0)
                    {
                        if (desiredY < height && desiredY >= 0)
                        {
                            Tile tile = tiles[desiredX, desiredY];
                            if (tile.isMine)
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }
    }
}