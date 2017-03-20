using UnityEngine;
using System.Collections;

namespace Pathfinder
{
    public class NodeInput : MonoBehaviour
    {
        [Header("Assign / Quicksetup")]
        public Node ParentNode;

        [ContextMenu("QuickSetup")]
        void _QuickSetup()
        {
            ParentNode = GetComponentInParent<Node>();
        }


        public void OnMouseUpAsButton()
        {
            ParentNode.OnNodeSelected();
        }
    }
}