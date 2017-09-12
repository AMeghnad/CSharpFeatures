using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Grid : MonoBehaviour
    {
        public enum MineState
        {
            LOSS = 0,
            WIN = 1
        }
        public enum MouseButton
        {
            LEFT_MOUSE = 0,
            RIGHT_MOUSE = 1,
            MIDDLE_MOUSE = 2
        }
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
            if (Input.GetMouseButtonDown((int)MouseButton.LEFT_MOUSE))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Perform the raycast
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // Check if something was hit
                if (hit.collider != null)
                {
                    // LET hitTile = hit collider's Tile component
                    Tile t = hit.collider.GetComponent<Tile>();
                    // IF hitTile != null
                    if (t != null)
                    {
                        // CALL SelectTile(hitTile)
                        SelectTile(t);
                    }
                }
            }

            // Use flag on GetMouseButtonDown(RIGHT_MOUSE)
            if (Input.GetMouseButtonDown((int)MouseButton.RIGHT_MOUSE))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Perform the raycast
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                // Check if something was hit
                if (hit.collider != null)
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if(t != null)
                    {
                        if (!t.isRevealed)
                        {
                            t.ToggleFlag();
                        }
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

        // Count adjacent mines at element
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

                    // IF desiredX and desiredY is within range of the tiles array length
                    if (desiredX < width && desiredX >= 0)
                    {
                        if (desiredY < height && desiredY >= 0)
                        {
                            Tile tile = tiles[desiredX, desiredY];
                            // If the tile is a mine
                            if (tile.isMine)
                            {
                                // Increase count by 1
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        public void FFuncover(int x, int y, bool[,] visited)
        {
            // IF x >= 0 AND y >= 0 AND x < width AND y < height
            if(x >= 0 && y >=0 && x < width && y < height)
            {
                // IF visited[x,y]
                if (visited[x, y])
                {
                    // RETURN
                    return;
                }
                // LET tile = tiles[x,y]
                Tile tile = tiles[x, y];
                // LET adjacentMines = GetAdjacentMineCountAt(tile)
                int adjacentMines = GetAdjacentMineCountAt(tile);
                // CALL tile.Reveal(adjacentMines)
                tile.Reveal(adjacentMines);

                // IF adjacentMines > 0
                if(adjacentMines > 0)
                {
                    // RETURN
                    return;
                }

                // SET visited[x,y] = true
                visited[x, y] = true;

                // CALL FFuncover(x - 1, y, visited)
                FFuncover(x - 1, y, visited);
                // CALL FFuncover(x + 1, y, visited)
                FFuncover(x + 1, y, visited);
                // CALL FFuncover(x, y - 1, visited)
                FFuncover(x, y - 1, visited);
                // CALL FFuncover(x, y + 1, visited)
                FFuncover(x, y + 1, visited);
            }
        }

        // Uncovers all mines that are in the grid
        public void UncoverMines(int mineState)
        {            
            // FOR x = 0 to x < width
            for(int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    // LET currentTile = tiles[x,y]
                    Tile currentTile = tiles[x, y];
                    // IF currentTile isMine
                    if (currentTile.isMine)
                    {
                        // LET adjacentMines = GetAdjacentMineCountAt(currentTile)
                        int adjacentMines = GetAdjacentMineCountAt(currentTile);
                        // CALL currentTile.Reveal(adjacentMines, mineState)
                        currentTile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        // Detects if there are no more empty tiles in the game
        bool NoMoreEmptyTiles()
        {
            // LET emptyTileCount = 0
            int emptyTileCount = 0;
            // FOR x = 0 to x < width
            for (int x = 0; x < width; x++)
            {
                // FOR y = 0 to y < height
                for (int y = 0; y < height; y++)
                {
                    //LET currentTile = tiles[x,y]
                    Tile currentTile = tiles[x, y];
                    // IF !currentTile.isRevealed AND !currentTile.isMine
                    if (!currentTile.isRevealed && !currentTile.isMine)
                    {
                        // SET emptyTileCount = emptyTileCount + 1
                        emptyTileCount += 1;
                    }
                }
            }
            return emptyTileCount == 0;
        }

        // Takes in a tile selected by the user in some way to reveal it
        public void SelectTile(Tile selectedTile)
        {
            // LET adjacentMines = GetAdjacentMineCountAt(selectedTile)
            int adjacentMines = GetAdjacentMineCountAt(selectedTile);
            // CALL selectedTile.Reveal(adjacentMines)
            selectedTile.Reveal(adjacentMines);
            // IF selectedTile isMine
            if (selectedTile.isMine)
            {
                // CALL UncoverMines(0)
                UncoverMines(0);
                // [EXTRA] Perform Game Over Logic
                print("Game Over man, Game Over!");
            }
            // ELSEIF adjacentMines == 0
            else if(adjacentMines == 0)
            {
                // LET x = selectedTile.x
                int x = selectedTile.x;
                // LET y = selectedTile.y
                int y = selectedTile.y;
                // CALL FFuncover(x, y, new bool[width,height])
                FFuncover(x, y, new bool[width, height]);
            }
            // IF NoMoreEmptyTiles()
            if (NoMoreEmptyTiles())
            {
                // CALL UncoverMines(1)
                UncoverMines(1);
                // [EXTRA] Perform Win Logic
                print("You Win!");
            }
        }
    }
}