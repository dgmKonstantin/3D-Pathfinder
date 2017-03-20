using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class Pathfinder : MonoBehaviour
    {
        [Header("Prefabs")]
        public Agent AgentPrefab;
        public WayPoint WayPointPrefab;

        [Header("RunTime")]
        public List<Node> SelectedNodes;
        public Ground CurrentGround;
        public Material WayPointMaterial;


        public void OnGroundNodeSelected(Node node)
        {
            SelectedNodes = SelectedNodes ?? new List<Node>();
            if (SelectedNodes.Count == 1)
            {
                CurrentGround.ClearWayPoints();

                var path = _SearchRoute(SelectedNodes[0], node);

                _HighLightPath(path);
                _CreateStreets(path);
                SelectedNodes.Clear();
            }
            else if (SelectedNodes.Count == 0)
            {
                _CreateNewWayPoint(node);
                SelectedNodes.Add(node);
            }
        }

        List<Edge> _SearchRoute(Node start, Node end)
        {
            var searchingNodes = new PrioQueue();
            searchingNodes.Enqueue(start,0);

            var path = new List<Edge>();
            var visitedNodes = new List<Node>();
            var counter = 0;

            visitedNodes.Add(start);

            while (searchingNodes.Count > 0 && counter < 100)
            {
                var searchNode = searchingNodes.Dequeue();

                if (searchNode.IsSameLocation(end)) return path;

                var edgeNeighbours = searchNode.EdgesToNeighbours;
                var pathEdge = _GetCheapestNeighbour(edgeNeighbours, visitedNodes, searchingNodes, searchNode, start, end);
                if (pathEdge == null)
                {
                    Debug.LogWarning("No Path available");
                    return new List<Edge>();
                }

                path.Add(pathEdge);
                visitedNodes.Add(pathEdge.NodeB);

                //searchingNodes.Enqueue(pathEdge.NodeB);
                counter++;
            }

            return path;
        }

        Edge _GetCheapestNeighbour(List<Edge> neighbours, List<Node> visitedNodes, PrioQueue searchingNodes,Node current, Node start, Node end)
        {
            var shortest = 0f;
            Edge result = null;

            for (int c = 0; c < neighbours.Count; c++)
            {
                var edge = neighbours[c];

                if (visitedNodes.Contains(edge.NodeB)) continue;
                if (!edge.NodeB.IsWalkable) continue;

                edge.CostG = _GetDistance(start, edge.NodeB);
                edge.CostH = _GetDistance(end, edge.NodeB);

                var cost = edge.CostF;
                if (shortest == 0 || cost < shortest)
                {
                    searchingNodes.Enqueue(edge.NodeB, cost);
                    shortest = cost;
                    result = edge;
                    edge.Parent = current;
                }
                else if (cost == shortest && result.CostH > edge.CostH)
                {
                    result = edge;
                    searchingNodes.Enqueue(edge.NodeB, cost);
                    edge.Parent = current;
                }
            }
            return result;
        }

        List<Node> _GetPathsFromNodes(List<Node> searchedNodes, Node end)
        {
            var distances = new List<float>();

            for (int c = 0; c < searchedNodes.Count; c++)
            {
                var searchNode = searchedNodes[c];
            }
            return null;
        }

        int _GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Mathf.Abs(nodeA.LocationX - nodeB.LocationX);
            int dstY = Mathf.Abs(nodeA.LocationZ - nodeB.LocationZ);

            return dstX + dstY;
        }


        void _HighLightPath(List<Edge> edges)
        {
            for (int c = 0; c < edges.Count; c++)
            {
                var edge = edges[c];
                var node = edge.NodeA;

                _CreateNewWayPoint(edge.NodeA);

                if ((c + 1) == edges.Count)//Die letzte Edge des Pfades muss bei endpunkte zeichnen NodeA & NodeB
                {
                    _CreateNewWayPoint(edge.NodeB);
                }
            }
        }

        void _CreateNewWayPoint(Node node)
        {
            var waypoint = Instantiate(WayPointPrefab.gameObject).GetComponent<WayPoint>();

            if (WayPointMaterial != null) waypoint.SetMaterial(WayPointMaterial);

            CurrentGround.AddWayPoint(waypoint, node);
            node.IsWalkable = false;
            if (node is Road)
            {
                var road = node as Road;
                road.RoadType = RoadType.Any;
            }
        }

        void _CreateStreets(List<Edge> edges)
        {
            for (int c = 0; c < edges.Count; c++)
            {
                var edge = edges[c];
                var node = edge.NodeA;
                var road = node as Road;

                road.RoadTypeByNeighbours();
                _UpdateRoadNeighbours(node.GetNeighbours());

                if ((c + 1) == edges.Count)//Die letzte Edge des Pfades muss bei endpunkte zeichnen NodeA & NodeB
                {
                    road = edge.NodeB as Road;
                    road.RoadTypeByNeighbours();
                    _UpdateRoadNeighbours(edge.NodeB.GetNeighbours());
                }
            }
        }

        void _UpdateRoadNeighbours(List<Node> neighbours)
        {
            for (int c = 0; c < neighbours.Count; c++)
            {
                var node = neighbours[c];
                var road = node as Road;

                if (road.RoadType == RoadType.Any) continue;
                if (road.RoadType == RoadType.PLAZA) continue;

                road.RoadTypeByNeighbours();
            }
        }


        public void AddNewGround(Ground ground)
        {
            CurrentGround = ground;
        }


    }
}