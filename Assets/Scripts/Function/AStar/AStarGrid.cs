using System.Collections.Generic;
using UnityEngine;

namespace Function
{
    public class AStarGrid : MonoBehaviour
    {
        // 检视面板设置
        public LayerMask obstacleLayer;

        // 检视面板或外部类设置
        public Vector2 mapSize;
        
        // 检视面板设置
        public float nodeRadius;
        protected float nodeDiameter;

        protected AStarNode[,] grid;
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
            // 计算节点直径
            nodeDiameter = nodeRadius * 2;
            // 计算网格各自数量
            gridWidth = Mathf.RoundToInt(mapSize.x / nodeDiameter);
            gridHeight = Mathf.RoundToInt(mapSize.y / nodeDiameter);
            grid = new AStarNode[gridWidth, gridHeight];
            // 计算左下角原点
            Vector3 worldBottomLeft = transform.position - new Vector3(mapSize.x / 2, mapSize.y / 2, 0);

            // 添加所有格子
            for (int x = 0; x < gridWidth; ++x) {
                for (int y = 0; y < gridHeight; ++y) {
                    // 计算格子中心点的世界坐标
                    Vector3 worldPos = worldBottomLeft + 
                                      new Vector3(x * nodeDiameter + nodeRadius, y * nodeDiameter + nodeRadius, 0);
                    // 通过 2D 碰撞检测，判断格子是否可以移动
                    bool walkable = !Physics2D.OverlapCircle(worldPos, nodeRadius, obstacleLayer);

                    grid[x, y] = new AStarNode(x, y, worldPos, walkable);
                }
            }
        }

        // 不检查坐标合法性
        public AStarNode NodeFromWorldPos(Vector3 worldPos)
        {
            // 计算出坐标点在地图中所占比例
            float xPercent = Mathf.Clamp01((worldPos.x + mapSize.x / 2) / mapSize.x);
            float yPercent = Mathf.Clamp01((worldPos.x + mapSize.y / 2) / mapSize.y);
            // 根据比例，计算出格子坐标
            int x = Mathf.RoundToInt((gridWidth - 1) * xPercent);
            int y = Mathf.RoundToInt((gridHeight - 1) * yPercent);
            
            return grid[x, y];
        }

        public List<AStarNode> GetNeighbors(AStarNode node)
        {
            List<AStarNode> neighbors = new List<AStarNode>();
            for (int x = -1; x <= 1; ++x) {
                for (int y = -1; y <= 1; ++y) {
                    if (x == 0 && y == 0)
                        continue;
                    int tx = node.gridX + x;
                    int ty = node.gridY + y;
                    if (tx >= 0 && tx < gridWidth && ty >= 0 && ty < gridHeight) {
                        neighbors.Add(grid[x, y]);
                    }
                }
            }

            return neighbors;
        }

        public void SetPath(List<AStarNode> path)
        {
            this.path = path;
        }

        protected void OnDrawGizmos()
        {
            if (displayGrid) {
                Gizmos.DrawWireCube(transform.position, new Vector3(mapSize.x, mapSize.y, 0));
                if (grid != null) {
                    foreach (var node in grid) {
                        Gizmos.color = node.walkable
                            ? new Color(0, 1, 0, displayAlpha)
                            : new Color(1, 0, 0, displayAlpha);
                        if (path != null && path.Contains(node)) {
                            Gizmos.color = Color.yellow;
                        }
                        Gizmos.DrawCube(node.worldPos, Vector3.one * (nodeDiameter - 0.1f));
                    }
                }
            }
        }
    }
}
