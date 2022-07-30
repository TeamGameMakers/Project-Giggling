using UnityEngine;
using CodeMonkey.Utils;

namespace GridSystem
{
    public class Grid
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private int[,] _gridArray;

        public Grid(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            
            _gridArray = new int[width, height];
            
            for (int x = 0; x < width; x ++)
            {
                for (int y = 0; y < height; y++)
                {
                    UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null, GetWorldPosition(x, y),
                        20, Color.white, TextAnchor.MiddleCenter);
                }
            }
        }

        private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y) * _cellSize;

    }
}