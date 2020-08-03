using Assets.Scripts.Geometry;
using Assets.Scripts.Spawn.TangentProjection;
using System.Collections.Generic;
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


        public void Move()
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
             {
                geometryManager.AdjustMovement(
                    o_mouse.selectedObjet,
                    cameraTransform.gameObject.GetComponent<Camera>().transform,
                    spawner.GetConnectionsPoints(o_mouse.selectedObjet),
                    cameraTransform.rotation.eulerAngles + 
                    TangentProjection.ProjectedAngle(cameraTransform.gameObject.GetComponent<Camera>(), o_mouse.clickCoords_s));

                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }
    }
}

    

