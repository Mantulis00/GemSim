﻿using Assets.Scripts.Geometry;
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


        public void Move(GameObject go2, float magnitude)
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
             {
                geometryManager.AdjustMovement(o_mouse.selectedObjet, go2, magnitude);

                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }
    }
}

    

