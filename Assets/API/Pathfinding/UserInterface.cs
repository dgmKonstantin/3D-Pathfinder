using UnityEngine;
using System.Collections;

namespace Pathfinder
{
    public class UserInterface : MonoBehaviour
    {
        public APIController MainController;

        public void RouteBlueClick()
        {
            MainController.Pathfinder.WayPointMaterial = MainController.APIGlobals.BlueRouteMat;
        }

        public void RouteYellowClick()
        {
            MainController.Pathfinder.WayPointMaterial = MainController.APIGlobals.YellowRouteMat;
        }

        public void RouteRedClick()
        {
            MainController.Pathfinder.WayPointMaterial = MainController.APIGlobals.RedRouteMat;
        }

        public void ClearClick()
        {
            var ground = MainController.Pathfinder.CurrentGround;
            ground.ResetGround();
        }
    }
}