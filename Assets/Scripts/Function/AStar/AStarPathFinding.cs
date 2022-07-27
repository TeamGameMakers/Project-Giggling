using System.Collections.Generic;
using UnityEngine;

namespace Function
{
    public class AStarPathFinding : MonoBehaviour
    {
        // // 地图所有格子
        // private readonly AStarNode[,] m_nodes;
        // // 地图宽高
        // private readonly int m_width;
        // private readonly int m_height;
        // // 开启列表
        // private readonly List<AStarNode> m_openList = new List<AStarNode>();
        // // 关闭列表
        // private readonly List<AStarNode> m_closeList = new List<AStarNode>();
        //
        // // 数组维度顺序：[x,y]
        // public AStarProcessor(AStarNode[,] nodes)
        // {
        //     m_nodes = nodes;
        //     m_width = nodes.GetLength(0);
        //     m_height = nodes.GetLength(1);
        // }
        //
        // // 传入点需要在地图内，不进行违法检测
        // public List<AStarNode> FindPath(Vector2Int startPos, Vector2Int endPos)
        // {
        //     AStarNode start = m_nodes[startPos.x, startPos.y];
        //     AStarNode end = m_nodes[endPos.x, endPos.y];
        //     // 判断节点的合法性
        //     if (start.type == NodeType.Obstacle || end.type == NodeType.Obstacle) {
        //         Debug.Log("起点或终点为障碍");
        //         return null;
        //     }
        //     
        //     // 清空列表
        //     m_openList.Clear();
        //     m_closeList.Clear();
        //     // 处理起点
        //     start.father = null;
        //     start.disStart = 0;
        //     start.disEnd = 0;
        //     start.cost = 0;
        //     m_closeList.Add(start);
        //
        //     do {
        //         // 遍历周围的点
        //         for (int x = -1; x <= 1; ++x) {
        //             for (int y = -1; y <= 1; ++y) {
        //                 if (x != 0 && y != 0) {
        //                     SearchNearlyNode(start.gridX + x, start.gridY + y, 1.4f, start, end);
        //                 }
        //                 else if (!(x == 0 && y == 0)) {
        //                     SearchNearlyNode(start.gridX + x, start.gridY + y, 1, start, end);
        //                 }
        //             }
        //         }
        //
        //         // 升序排序
        //         m_openList.Sort((a, b) => {
        //             if (a.disEnd > b.disEnd) {
        //                 return 1;
        //             }
        //             else {
        //                 return -1;
        //             }
        //         });
        //
        //         // 判断是否抵达终点
        //         if (start == end) {
        //             List<AStarNode> path = new List<AStarNode> { end };
        //             while (end.father != null) {
        //                 path.Add(end.father);
        //                 end = end.father;
        //             }
        //             path.Reverse();
        //             return path;
        //         }
        //         
        //         // 更改起点，进行下一轮搜索
        //         m_closeList.Add(m_openList[0]);
        //         start = m_openList[0];
        //         m_openList.RemoveAt(0);
        //     } while (m_openList.Count > 0);
        //
        //     Debug.Log("终点不可达");
        //     return null;
        // }
        //
        // private void SearchNearlyNode(int x, int y, float disStart, AStarNode father, AStarNode end)
        // {
        //     // 超出地图
        //     if (x < 0 || x >= m_width || y < 0 || y >= m_height)
        //         return;
        //     // 判断节点是否要处理
        //     AStarNode node = m_nodes[x, y];
        //     if (node == null || node.type == NodeType.Obstacle ||
        //         m_closeList.Contains(node) || m_openList.Contains(node))
        //         return;
        //     
        //     // 计算值
        //     node.father = father;
        //     node.disStart = father.disStart + disStart;
        //     // 原计算公式，会导致斜向和横向距离相等
        //     //node.disEnd = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        //     int dx = Mathf.Abs(end.gridX - node.gridX);
        //     int dy = Mathf.Abs(end.gridY - node.gridY);
        //     node.disEnd = Mathf.Sqrt(dx * dx + dy * dy);
        //     node.cost = node.disStart + node.disEnd;
        //     
        //     // 条件都通过了就加入开启列表
        //     m_openList.Add(node);
        // }
    }
}
