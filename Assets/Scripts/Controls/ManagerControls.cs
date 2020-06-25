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
            SpawnLine();
        }

        private void SpawnLine()
        {
            if ((o_mouse.a_que & ActionsQue.SpawnStart) == ActionsQue.SpawnStart)
            {
                o_mouse.ActionAddressed(ActionsQue.SpawnStart);
                spawner.MakeEndPoints(this.transform, o_mouse.selectedObjet, true);
            }
            if ((o_mouse.a_que & ActionsQue.SpawnFinish) == ActionsQue.SpawnFinish)
            {
                o_mouse.ActionAddressed(ActionsQue.SpawnFinish);
            }
        }


    }
}
