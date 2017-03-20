using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class Ground : MonoBehaviour
    {
        Node[,] _nodes;

        [Header("Header")]
        public Transform OwnTransform;

        [Header("RunTime")]
        public int Size;
        public List<WayPoint> WayPoints;

        internal void Setup(int size)
        {
            _nodes = new Node[size, size];
            Size = size;
        }

        internal void CreateNodes(Node nodePrefab)
        {
            _CreateGroundNodes(nodePrefab);
            _CreateNodeEdges();
        }

        void _CreateGroundNodes(Node nodePrefab)
        {
            var nodeSize = nodePrefab.GetSize();

            for (int x = 0; x < Size; x++)
            {
                for (int z = 0; z < Size; z++)
                {
                    var location = new Vector3(x, 0, z);
                    var node = Instantiate(nodePrefab.gameObject).GetComponent<Node>();

                    node.OwnTransform.parent = OwnTransform;
                    node.ParentGround = this;

                    var pos = new Vector3(location.x * nodeSize.x, location.y * nodeSize.y, location.z * nodeSize.z);

                    node.SetPosition(pos);
                    node.SetNodeMaterialBy(x + z);
                    node.SetLocationIndex(location);

                    _nodes[x, z] = node;
                }
            }

            OwnTransform.position = new Vector3(-Size / 2f, 0, -Size / 2f);
        }

        void _CreateNodeEdges()
        {
            for (int x = 0; x < _nodes.GetLongLength(0); x++)
            {
                for (int z = 0; z < _nodes.GetLongLength(1); z++)
                {
                    var node = _nodes[x, z];

                    Node nodeA;

                    if (HasNode(x + 1, z, out nodeA)) node.CreateEdgeTo(nodeA);
                    if (HasNode(x, z + 1, out nodeA)) node.CreateEdgeTo(nodeA);
                    if (HasNode(x - 1, z, out nodeA)) node.CreateEdgeTo(nodeA);
                    if (HasNode(x, z - 1, out nodeA)) node.CreateEdgeTo(nodeA);
                }
            }
        }

        public bool HasNode(int x, int z, out Node node)
        {
            node = null;

            if (x < 0 || z < 0) return false;
            else if (x >= Size || z >= Size) return false;
            else
            {
                node = _nodes[x, z];
                return true;
            }
        }


        public void AddWayPoint(WayPoint point, Node placeOn)
        {
            point.OwnTransform.parent = OwnTransform;
            point.ParentGround = this;
            point.SetPosition(placeOn.OwnTransform.position);

            WayPoints = WayPoints ?? new List<WayPoint>();
            WayPoints.Add(point);
        }


        public void ClearWayPoints()
        {
            for (int c = 0; c < WayPoints.Count; c++)
            {
                Destroy(WayPoints[c].gameObject);
            }
            WayPoints.Clear();
        }


        public void ResetGround()
        {
            for (int x = 0; x < _nodes.GetLongLength(0); x++)
            {
                for (int z = 0; z < _nodes.GetLongLength(1); z++)
                {
                    var node = _nodes[x, z];
                    node.ResetNode();
                    node.IsWalkable = true;
                }
            }
            ClearWayPoints();
        }
    }
}