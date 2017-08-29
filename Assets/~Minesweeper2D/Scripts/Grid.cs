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
        void Update()
        {

        }

        Tile SpawnTile(Vector3 pos)
        {
            // Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; // Position tile
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
                for (int y = 0; y< height; y++)
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
    }
}
