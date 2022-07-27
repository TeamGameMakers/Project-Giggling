using System;
using System.Collections.Generic;
using UnityEngine;

namespace Function
{
    public class AStarPathFinding : MonoBehaviour
    {
        protected AStarGrid grid;

        public Transform start;
        public Transform end;

        protected void Awake()
        {
            grid = GetComponent<AStarGrid>();
        }

        protected void FindPath(Vector3 startPos, Vector3 endPos)
        {
            Vector3[] wayPoints = Array.Empty<Vector3>();
            bool pathFound = false;

            AStarNode startNode = grid.NodeFromWorldPos(startPos);
            AStarNode targetNode = grid.NodeFromWorldPos(endPos);

            if (startNode.walkable && targetNode.walkable) {
                List<AStarNode> openList = new List<AStarNode>();
                HashSet<AStarNode> closeSet = new HashSet<AStarNode>();
                
                openList.Add(startNode);

                while (openList.Count > 0) {
                    // 查找最优的点
                    AStarNode currentNode = openList[0];
                    for (int i = 1; i < openList.Count; i++) {
                        // 消耗更小 或者 离终点更近
                        if (openList[i].Cost < currentNode.Cost || 
                            openList[i].Cost == currentNode.Cost && openList[i].endCost < currentNode.endCost) {
                            currentNode = openList[i];
                        }
                    }
                    // 从打开列表移入关闭列表
                    openList.Remove(currentNode);
                    closeSet.Add(currentNode);

                    // 抵达终点
                    if (currentNode == targetNode) {
                        pathFound = true;
                        break;
                    }

                    // 遍历周围
                    foreach (AStarNode neighbor in grid.GetNeighbors(currentNode)) {
                        if (!neighbor.walkable || closeSet.Contains(neighbor)) {
                            continue;
                        }

                        // 计算到临近点的消耗
                        int costToNeighbor = currentNode.startCost + GetDistance(currentNode, neighbor);
                        // 如果消耗不超过返回起点 或者 打开列表没有
                        if (costToNeighbor < neighbor.startCost || !openList.Contains(neighbor)) {
                            // 更新消耗
                            neighbor.startCost = costToNeighbor;
                            neighbor.endCost = GetDistance(neighbor, targetNode);
                            neighbor.parent = currentNode;

                            if (!openList.Contains(neighbor)) {
                                openList.Add(neighbor);
                            }
                        }
                    }
                }
            }
            
            if (pathFound) {
                wayPoints = RetracePath(startNode, targetNode);
            }
            else {
                Debug.Log("没有找到路径");
            }
        }
        
        protected Vector3[] RetracePath(AStarNode start, AStarNode end)
        {
            List<AStarNode> path = new List<AStarNode>();
            AStarNode currentNode = end;

            while (currentNode != start) {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            grid.SetPath(path);
            return SimplifyPath(path);
        }

        // 简化路径，将同方向移动的路径去除
        protected Vector3[] SimplifyPath(List<AStarNode> path) 
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;

            // 除开起点，开始遍历所有路径点
            for (int i = 1; i < path.Count; i++) {
                // 得出新的移动方向
                Vector2 directionToNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
                // 方向不一致，就添加路径点
                if (directionToNew != directionOld) {
                    wayPoints.Add(path[i].worldPos);
                }
                directionOld = directionToNew;
            }

            return wayPoints.ToArray();
        }

        protected int GetDistance(AStarNode a, AStarNode b)
        {
            int disX = Mathf.Abs(a.gridX - b.gridX);
            int disY = Mathf.Abs(a.gridY - b.gridY);

            // 近似计算？以短边作正方形，计算对角线长度，加上两边差
            if (disX > disY)
                return disY * 14 + 10 * (disX - disY);
            return disX * 14 + 10 * (disY - disX);
        }
    }
}
