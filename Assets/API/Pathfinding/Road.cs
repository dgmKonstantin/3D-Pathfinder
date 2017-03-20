using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class Road : Node
    {
        public RoadType RoadType;

        public void RoadTypeByNeighbours()
        {
            var roadTypes = _NeighboursToRoadType();
            var ownRoadType = APIController.Current.APIGlobals.RoadTextures.NeighboursToRoad(roadTypes);
            var roadTexture = APIController.Current.APIGlobals.RoadTextures.RoadTypeToTexture(ownRoadType);

            RoadType = ownRoadType;
            UpdateTexture(roadTexture);
        }

        List<bool> _NeighboursToRoadType()
        {
            var result = new List<bool>();
            var x = LocationX;
            var z = LocationZ;

            Node nodeA;
            var tmpNodes = new List<Node>() { null, null, null, null };

            if (ParentGround.HasNode(x + 1, z, out nodeA)) tmpNodes[0] = nodeA;
            if (ParentGround.HasNode(x, z + 1, out nodeA)) tmpNodes[1] = nodeA;
            if (ParentGround.HasNode(x - 1, z, out nodeA)) tmpNodes[2] = nodeA;
            if (ParentGround.HasNode(x, z - 1, out nodeA)) tmpNodes[3] = nodeA;

            for (int c = 0; c < tmpNodes.Count; c++)
            {
                var road = tmpNodes[c] as Road;
                var type = RoadType.PLAZA;

                if (road != null)
                {
                    type = road.RoadType;
                }

                result.Add(type != RoadType.PLAZA);
            }

            return result;
        }


        public override void ResetNode()
        {
            RoadType = RoadType.PLAZA;
            var roadTexture = APIController.Current.APIGlobals.RoadTextures.RoadTypeToTexture(RoadType);
            UpdateTexture(roadTexture);
            base.ResetNode();
        }
    }
}