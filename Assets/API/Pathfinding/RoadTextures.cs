using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinder
{
    public enum RoadType
    {
        EW,
        NE,
        NEWS,
        NS,
        NW,
        PLAZA,
        SE,
        SW,
        TE,
        TN,
        TS,
        TW,
        Any
    }

    [System.Serializable]
    public struct NeighbourRoadMatrix
    {
        public bool[] NeighbourSet;
        public RoadType Type;
    }

    public class RoadTextures : MonoBehaviour
    {
        public Texture RoadEW;
        public Texture RoadNE;
        public Texture RoadNEWS;
        public Texture RoadNS;
        public Texture RoadNW;
        public Texture RoadPLAZA;
        public Texture RoadSE;
        public Texture RoadSW;
        public Texture RoadTE;
        public Texture RoadTN;
        public Texture RoadTS;
        public Texture RoadTW;

        public NeighbourRoadMatrix[] RoadMatrix;

        public Texture RoadTypeToTexture(RoadType ty)
        {
            switch (ty)
            {
                case RoadType.EW:
                    return RoadEW;
                case RoadType.NE:
                    return RoadNE;
                case RoadType.NEWS:
                    return RoadNEWS;
                case RoadType.NS:
                    return RoadNS;
                case RoadType.NW:
                    return RoadNW;
                case RoadType.PLAZA:
                    return RoadPLAZA;
                case RoadType.SE:
                    return RoadSE;
                case RoadType.SW:
                    return RoadSW;
                case RoadType.TE:
                    return RoadTE;
                case RoadType.TN:
                    return RoadTN;
                case RoadType.TS:
                    return RoadTS;
                case RoadType.TW:
                    return RoadTW;
                default:
                    return RoadPLAZA;
            }
        }

        public RoadType NeighboursToRoad(List<bool> roadNeighbours)
        {
            for (int c = 0; c < RoadMatrix.Length; c++)
            {
                var mat = RoadMatrix[c];
                var comparArr = mat.NeighbourSet;
                if(_CompareRoadMatrix(roadNeighbours,comparArr))
                {
                    return mat.Type;
                }
            }
            return RoadType.PLAZA;
        }

        bool _CompareRoadMatrix(IList<bool> a, IList<bool> b)
        {
            int k = 0;
            return a.All(x => x.Equals(b[k++]));
        }
    }
}