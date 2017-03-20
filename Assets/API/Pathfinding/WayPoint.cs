using UnityEngine;
using System.Collections;

namespace Pathfinder
{
    public class WayPoint : MonoBehaviour
    {
        [Header("Assign / Quicksetup")]
        public Transform OwnTransform;
        public MeshRenderer OwnMeshRenderer;

        [Header("RunTime")]
        public Ground ParentGround;

        [ContextMenu("QuickSetup")]
        void _QuickSetup()
        {
            OwnTransform = transform;
            OwnMeshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetPosition(Vector3 pos)
        {
            OwnTransform.position = pos;
        }

        public void SetMaterial(Material mat)
        {
            OwnMeshRenderer.material = mat;
        }
    }
}