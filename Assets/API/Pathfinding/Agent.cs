using UnityEngine;
using System.Collections;

namespace Pathfinder
{
    public class Agent : MonoBehaviour
    {
        [Header("Assign / Quicksetup")]
        public Transform OwnTransform;
        public MeshRenderer OwnMeshRenderer;

        [ContextMenu("QuickSetup")]
        void _QuickSetup()
        {
            OwnTransform = transform;
            OwnMeshRenderer = GetComponent<MeshRenderer>();
        }
    }
}