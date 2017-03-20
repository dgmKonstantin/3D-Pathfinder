using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class GroundGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        public Node GroundNodePrefab;
        public Ground GroundPrefab;

        [Header("Settings")]
        public int Size = 10;


        [ContextMenu("Generate Ground")]
        public void GenerateGround()
        {
            var ground = _CreateGroundContainer();
            ground.Setup(Size);
            ground.CreateNodes(GroundNodePrefab);

            if (Application.isEditor)
            {
                GameObject.FindObjectOfType<APIController>().Pathfinder.AddNewGround(ground);
            }
            else
            {
                APIController.Current.Pathfinder.AddNewGround(ground);
            }
        }

        Ground _CreateGroundContainer()
        {
            return Instantiate(GroundPrefab.gameObject).GetComponent<Ground>();
        }
    }
}