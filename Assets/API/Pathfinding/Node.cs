using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class Node : MonoBehaviour
    {
        [Header("Assign / Quicksetup")]
        public Transform OwnTransform;
        public MeshRenderer OwnMeshRenderer;
        public BoxCollider OwnBoxCollider;
        public NodeInput NodeInput;

        public Material EvenMat;
        public Material OddMat;

        [Header("RunTime")]
        public Ground ParentGround;
        public int LocationX;
        public int LocationZ;
        public List<Edge> EdgesToNeighbours;
        public bool IsWalkable = true;

        [ContextMenu("QuickSetup")]
        void _QuickSetup()
        {
            OwnTransform = transform;
            OwnMeshRenderer = GetComponent<MeshRenderer>();
            OwnBoxCollider = GetComponent<BoxCollider>();
            NodeInput = GetComponent<NodeInput>();

            EdgesToNeighbours = new List<Edge>();
        }


        public void SetPosition(Vector3 pos)
        {
            OwnTransform.position = pos;
        }

        public void SetNodeMaterialBy(int oddEvenIndex)
        {
            //if(oddEvenIndex == 0 || oddEvenIndex % 2 == 0)
            //{
            //    OwnMeshRenderer.material = EvenMat;
            //}
            //else
            //{
            //    OwnMeshRenderer.material = OddMat;
            //}
        }

        public void SetLocationIndex(Vector3 loca)
        {
            LocationX = (int)loca.x;
            LocationZ = (int)loca.z;
            name += ".Location." + LocationX + "." + LocationZ;
        }


        public void CreateEdgeTo(Node nodeA)
        {
            EdgesToNeighbours = EdgesToNeighbours ?? new List<Edge>();
            EdgesToNeighbours.Add(Edge.Create(this, nodeA));
        }


        public bool IsSameLocation(Node node)
        {
            return this.LocationX == node.LocationX && this.LocationZ == node.LocationZ;
        }

        public List<Node> GetNeighbours()
        {
            var result = new List<Node>();
            for (int c = 0; c < EdgesToNeighbours.Count; c++)
            {
                var edge = EdgesToNeighbours[c];
                result.Add(edge.NodeB);
            }
            return result;
        }


        public void OnNodeSelected()
        {
            APIController.Current.Pathfinder.OnGroundNodeSelected(this);
        }


        public Vector3 GetSize()
        {
            return OwnTransform.localScale;
        }

        public void UpdateTexture(Texture tex)
        {
            var mat = OwnMeshRenderer.material;
            mat.SetTexture("_MainTex", tex);
            OwnMeshRenderer.material = mat;
        }

        public virtual void ResetNode()
        {
            IsWalkable = true;
        }
    }
}