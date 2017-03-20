using UnityEngine;
using System.Collections;

namespace Pathfinder
{
    [System.Serializable]
    public class Edge
    {
        public Node Parent;

        public Node NodeA;
        public Node NodeB;

        /// <summary>
        /// Costs 
        /// https://www.raywenderlich.com/4946/introduction-to-a-pathfinding
        /// </summary>
        public int CostF
        {
            get { return CostG + CostH; }
        }
        /// <summary>
        /// G is the movement cost from the start point A to the current square.
        /// </summary>
        public int CostG;
        /// <summary>
        /// H is the estimated movement cost from the current square to the destination point 
        /// </summary>
        public int CostH;

        public static Edge Create(Node nodeA, Node nodeB)
        {
            var edge = new Edge();
            edge.NodeA = nodeA;
            edge.NodeB = nodeB;
            return edge;
        }
    }
}