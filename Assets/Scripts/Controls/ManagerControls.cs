using Assets.Scripts.Controls.Keyboard;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ManagerControls : MonoBehaviour
    {
        public SpawnerManager spawner;

        O_Camera o_camera;
        O_Mause o_mouse;
        O_Keyboard o_keyboard;
        EditMode editMode;


        private void Start()
        {
            o_camera = new O_Camera(Camera.main);
            o_mouse = new O_Mause();
            o_keyboard = new O_Keyboard();

            editMode = new EditMode(o_mouse, this.transform, spawner);
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
                    editMode.Spawn();
                    
                else if (o_keyboard.action == KeyboardActions.Move)
                    editMode.Move();
            }
        }



    }
}
