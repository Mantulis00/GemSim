using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class EditMode
    {
        Vector3 startPos, finishPos;
        O_Mause o_mouse;
        Transform cameraTransform;
        SpawnerManager spawner;

        public EditMode(O_Mause o_mouse, Transform managerTransform, SpawnerManager spawner)
        {
            this.o_mouse = o_mouse;
            this.cameraTransform = managerTransform;
            this.spawner = spawner;
        }


        public  void Spawn()
        {

                if ((o_mouse.a_que & ActionsQue.SpawnStart) == ActionsQue.SpawnStart)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObject)) // cant spawn connector
                {
                    startPos = spawner.MovePoint(
                        spawner.MakeGO(o_mouse.selectedObject, 
                        o_mouse.CheckForObject(), 
                        SpawnOptions.Start),
                        cameraTransform, o_mouse.clickCoords_s);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnStart);
            }
            if ((o_mouse.a_que & ActionsQue.SpawnFinish) == ActionsQue.SpawnFinish)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObject))
                {
                    finishPos = spawner.MovePoint(
                        spawner.MakeGO(
                        o_mouse.selectedObject, o_mouse.CheckForObject(),
                        SpawnOptions.Finish), 
                        cameraTransform, 
                        o_mouse.clickCoords_f);


                    SpawnerManager.MoveConnection( // this will come here
                        spawner.MakeGO(o_mouse.selectedObject, o_mouse.CheckForObject(), SpawnOptions.Connection),
                        startPos, finishPos);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnFinish);
            }
        }

        public void Move()
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObject))
                {
                    spawner.MovePoint(o_mouse.selectedObject, cameraTransform, o_mouse.clickCoords_s);

                }

                MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObject));
                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }


        private void MoveAdjustConnections(List<Spawn.Structures.Setup.Structure.connection> connections) // change for edit and sim modes
        {
            foreach (Spawn.Structures.Setup.Structure.connection c in connections.ToList())
            {

                /// gtfo this somewhere else
                double lenght = Math.Round((c.endPoint.transform.position - o_mouse.selectedObject.transform.position).magnitude, 4);
                Spawn.Structures.Setup.Structure.connection ce = spawner.GetStructure(c.endPoint).FindOtherSideOfConnection(o_mouse.selectedObject, c.endPoint);

                ce.dataConnection.originalLenght = lenght; // does nothing
                ce.dataConnection.realLenght = lenght;

                Spawn.Structures.Setup.Structure.connection co = spawner.GetStructure(c.endPoint).FindOtherSideOfConnection(c.endPoint, o_mouse.selectedObject);
                co.dataConnection.originalLenght = lenght;
                co.dataConnection.realLenght = lenght;

                co.connector.GetComponent<Renderer>().material.color = Color.white;
                ////

                SpawnerManager.MoveConnection( // do this for every connector object has
                      c.connector,
                      c.endPoint.transform.position,
                      o_mouse.selectedObject.transform.position);

            }

        }


    }
}
