using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinder
{
    public class APIController : MonoBehaviour
    {
        public static APIController Current;

        public UserInterface MainUI;
        public APIGlobals APIGlobals;
        public Pathfinder Pathfinder;
        public GroundGenerator GroundGenerator;

        void Awake()
        {
            Current = this;
        }

        void Start()
        {
            GroundGenerator.GenerateGround();
        }
    }
}