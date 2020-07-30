using Assets.Scripts.Geometry;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes
{
    class SimulationMode
    {
        GeometryManager geometryManager;
        O_Mause o_mouse;
        SpawnerManager spawner;
        public SimulationMode(O_Mause o_mouse, SpawnerManager spawner)
        {
            geometryManager = new GeometryManager();
            this.o_mouse = o_mouse;
            this.spawner = spawner;
        }

        public void GiveType(GameObject go, GeometryType type)
        {
            geometryManager.GiveType(go, type);
        }


        public void Move(GameObject go, List<GameObject> connections, Transform cameraPos)
        {
            geometryManager.AdjustMovement(
                go, 
                connections, 
                spawner.MovePoint(null, cameraPos, o_mouse.clickCoords_s));
        }
    }
}

    

