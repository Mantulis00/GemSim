using Assets.Scripts.Geometry;
using Assets.Scripts.Spawn.Structures.Setup;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controls.Modes
{
    class SimulationMode
    {
        

        GeometryManager geometryManager;
        O_Mause o_mouse;
        Transform cameraTransform;
        SpawnerManager spawner;
        public SimulationMode(O_Mause o_mouse, Transform cameraTransform, SpawnerManager spawner)
        {
         

            geometryManager = new GeometryManager();
            this.o_mouse = o_mouse;
            this.cameraTransform = cameraTransform;
            this.spawner = spawner;
        }




        public void GiveType(GameObject go, GeometryType type)
        {
            geometryManager.GiveType(go, type);
        }


        public void Move(GameObject goAround, Structure structure)
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
             {
                geometryManager.AdjustMovement(o_mouse.selectedObject, goAround, structure);

                MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObject));
                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }


        private void MoveAdjustConnections(List<Structure.connection> connections) // change for edit and sim modes
        {
            foreach (Structure.connection c in connections.ToList())
            {

                SpawnerManager.MoveConnection( // do this for every connector object has
                      c.connector,
                      c.endPoint.transform.position,
                      o_mouse.selectedObject.transform.position);
            }

        }



    }
}

    

