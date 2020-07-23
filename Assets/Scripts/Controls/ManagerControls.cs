using Assets.Scripts.Controls.Keyboard;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ManagerControls : MonoBehaviour
    {
        public SpawnerManager spawner;

        O_Camera o_camera;
        O_Mause o_mouse;
        O_Keyboard o_keyboard;

        Vector3 startPos, finishPos;

        private void Start()
        {
            o_camera = new O_Camera(Camera.main);
            o_mouse = new O_Mause();
            o_keyboard = new O_Keyboard();
        }
        private void Update()
        {
            UpdateOStatus();

            if (o_mouse.a_que != 0)
                PerformActions();

        }

        private bool UpdateOStatus()
        {
            bool actionsExist = false;
            o_camera.Update();
            o_keyboard.Update(o_mouse.selectedObjet);
            o_mouse.Update(o_keyboard.action);

            return actionsExist;
        }

        private void PerformActions()
        {
            if (o_mouse.selectedObjet != null)
            {
                if (o_keyboard.action == KeyboardActions.Spawn)
                    SpawnLine();
                    
                else if (o_keyboard.action == KeyboardActions.Move)
                    MoveLine();
            }
        }

        private void SpawnLine()
        {

            if ((o_mouse.a_que & ActionsQue.SpawnStart) == ActionsQue.SpawnStart)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObjet))
                {
                    startPos = spawner.MovePoint(spawner.MakeGO(o_mouse.selectedObjet), this.transform, o_mouse.clickCoords_s);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnStart);
            }
            if ((o_mouse.a_que & ActionsQue.SpawnFinish) == ActionsQue.SpawnFinish)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObjet))
                {
                    finishPos = spawner.MovePoint(spawner.MakeGO(o_mouse.selectedObjet), this.transform, o_mouse.clickCoords_f);

                    spawner.MoveConnection(spawner.MakeGO(o_mouse.selectedObjet), startPos, finishPos);
                }

                o_mouse.ActionAddressed(ActionsQue.SpawnFinish);
            }
        }

        private void MoveLine()
        {
            if ((o_mouse.a_que & ActionsQue.Move) == ActionsQue.Move)
            {
                if (!spawner.CheckConnector(o_mouse.selectedObjet))
                {
                    spawner.MovePoint(o_mouse.selectedObjet, this.transform, o_mouse.clickCoords_s);

                    spawner.MoveConnection(
                        spawner.FindConnector(o_mouse.selectedObjet),
                        spawner.FindSecondPointLocation(o_mouse.selectedObjet).position,
                        o_mouse.selectedObjet.transform.position);
                }

                o_mouse.ActionAddressed(ActionsQue.Move);
            }
        }


    }
}
