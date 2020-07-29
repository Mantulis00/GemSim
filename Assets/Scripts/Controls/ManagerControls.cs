using Assets.Scripts.Controls.Keyboard;
using Assets.Scripts.Controls.Modes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    class ManagerControls : MonoBehaviour
    {
        public SpawnerManager spawner;
        internal Mode CurrentMode;

        private  O_Camera o_camera;
       private O_Mause o_mouse;
       private O_Keyboard o_keyboard;
       private EditMode editMode;
       private SimulationMode simulationMode;



        private void Start()
        {
            o_camera = new O_Camera(Camera.main);
            o_mouse = new O_Mause();
            o_keyboard = new O_Keyboard();

            editMode = new EditMode(o_mouse, this.transform, spawner);
            CurrentMode = Mode.Edit;
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
           if (CurrentMode == Mode.Edit)
            {
                if (o_mouse.selectedObjet != null)
                {
                    if (o_keyboard.action == KeyboardAction.Spawn)
                        editMode.Spawn();

                    else if (o_keyboard.action == KeyboardAction.Move)
                        editMode.Move();
                }
            }
           else if (CurrentMode == Mode.Simulate)
            {
                if (o_keyboard.action == KeyboardAction.Move)
                    simulationMode.Move();
            }
            
        }



    }
}
