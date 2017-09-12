using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    public class Tile : MonoBehaviour
    {

        // Store x and y coordinates in array for future use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // Is the current tile a mine?
        public bool isRevealed = false; // Has the tile already been revealed?
        [Header("References")]
        public Sprite[] emptySprites; // List of empty sprites ie, empty, 1, 2, 3, 4 etc.
        public Sprite[] mineSprites; // The mine sprites
        public Sprite defaultSprite;
        public Sprite flagSprite;

        private SpriteRenderer rend; // Reference to sprite renderer
        private bool isFlagged = false;

        void Awake()
        {
            // Grab reference to SpriteRenderer
            rend = GetComponent<SpriteRenderer>();
        }

        // Use this for initialization
        void Start()
        {
            // Randomly decide if it's a mine or not
            isMine = Random.value < .1f;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Reveal(int adjacentMines, int mineState = 0)
        {
            // Flag the tiles as being revealed
            isRevealed = true;
            // Checks if the tile is a mine
            if (isMine)
            {
                // Set sprite to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                // Sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }

        public void ToggleFlag()
        {
            isFlagged = !isFlagged;
            if (isFlagged)
            {
                // set rend.sprite to flagSprite
                rend.sprite = flagSprite;
            }
            else
            {
                // set rend.sprite to defaultSprite
                rend.sprite = defaultSprite;
            }

            
        }
    }
}
