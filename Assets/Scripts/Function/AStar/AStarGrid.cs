using System;
using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Function
{
    public class AStarGrid : MonoBehaviour
    {
        public LayerMask walkableLayer;
        public LayerMask obstacleLayer;

        public Vector2 gridSize;
        public float nodeRadius;

        protected AStarNode[,] grid;
        // 节点直径
        protected float nodeDiameter;
        protected int gridWidth;
        protected int gridHeight;
        public int MaxSize => gridWidth * gridHeight;

        protected List<AStarNode> path;
        
        [Header("测试参数")]
        public bool displayGrid = true;
        public float displayAlpha = 0.2f;

        protected void Awake()
        {
            if (grid == null) {
                CreateGrid();
            }
        }

        //[Button("CreateGrid")]
        protected void CreateGrid()
        {
            nodeDiameter = nodeRadius * 2;
            gridWidth = Mathf.RoundToInt(gridSize.x / nodeDiameter);
            gridHeight = Mathf.RoundToInt(gridSize.y / nodeDiameter);
            grid = new AStarNode[gridWidth, gridHeight];
        }
    }
}
