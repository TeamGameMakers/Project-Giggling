using UnityEngine;

namespace Function
{
    public class AStarNode
    {
        // 实际坐标
        public Vector2 worldPos;
        // 格子坐标
        public int gridX;
        public int gridY;
        // 寻路消耗
        public float Cost => startCost + endCost;
        // 至起点消耗
        public float startCost;
        // 至终点消耗
        public float endCost;
        // 能否通过
        public bool walkable;
        // 父格子
        public AStarNode parent;

        public AStarNode(int gridX, int gridY, Vector2 worldPos)
        {
            this.gridX = gridX;
            this.gridY = gridY;
            this.worldPos = worldPos;
        }
        
        // 用于排序的比较器
        public int CompareTo(AStarNode other)
        {
            // 比较总消耗
            int res = Cost.CompareTo(other.Cost);
            // 如果相等，就比较终点消耗
            if (res == 0) {
                res = endCost.CompareTo(other.endCost);
            }

            return -res;
        }
    }
}

