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
                if (!spawner.CheckConnector(o_mouse.selectedObjet)) // cant spawn connector
                {
                    startPos = spawner.MovePoint(
                        spawner.MakeGO(o_mouse.selectedObjet, o_mouse.CheckForObject(), SpawnOptions.Start),
                        cameraTransform, o_mouse.clickCoords_s);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnStart);
            }
            if ((o_mouse.a_que & ActionsQue.SpawnFinish) == ActionsQue.SpawnFinish)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObjet))
                {
                    finishPos = spawner.MovePoint(
                        spawner.MakeGO(o_mouse.selectedObjet, o_mouse.CheckForObject(),
                        SpawnOptions.Finish), 
                        cameraTransform, 
                        o_mouse.clickCoords_f);


                    spawner.MoveConnection(
                        spawner.MakeGO(o_mouse.selectedObjet, o_mouse.CheckForObject(), SpawnOptions.Connection),
                        startPos, finishPos);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnFinish);
            }
        }

        public void Move()
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObjet))
                {
                    spawner.MovePoint(o_mouse.selectedObjet, cameraTransform, o_mouse.clickCoords_s);

                    MoveAdjustConnections(spawner.GetConnections(o_mouse.selectedObjet));
                }

                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }

        private void MoveAdjustConnections(List<Spawn.Structures.Setup.Structure.connection> connections)
        {
            foreach (Spawn.Structures.Setup.Structure.connection c in connections.ToList())
            {
                spawner.MoveConnection( // do this for every connector object has
                      c.connector,
                      c.endPoint.transform.position,
                      o_mouse.selectedObjet.transform.position);
            }

        }
    }
}
