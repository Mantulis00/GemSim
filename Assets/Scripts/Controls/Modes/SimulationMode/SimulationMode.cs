using Assets.Scripts.Controls.Modes.Groups;
using Assets.Scripts.Geometry;
using Assets.Scripts.Geometry.Objects;
using Assets.Scripts.Spawn.Structures.Setup;
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


        public void Move(GameObject goAround, Structure structure)
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
             {
                geometryManager.AdjustMovement(o_mouse.selectedObjet, goAround, structure);

                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }

      /*  public void Enlist(GameObject go)
        {
            if ((o_mouse.a_que & ActionsQue.ListAdd) == ActionsQue.ListAdd)
            {
                group.Add(go, spawner.GetStructure(go));
                o_mouse.ActionAddressed(ActionsQue.ListAdd);
            }
            else if ((o_mouse.a_que & ActionsQue.ListRemove) == ActionsQue.ListRemove)
            {
                group.Remove(go, spawner.GetStructure(go));
                o_mouse.ActionAddressed(ActionsQue.ListRemove);
            }
        }*/
    }
}

    

