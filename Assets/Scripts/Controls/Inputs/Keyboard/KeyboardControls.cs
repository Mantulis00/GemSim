using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    static class KeyboardControls
    {
        public static KeyboardAction GetKeyboardAction()
        {
            KeyboardAction action = KeyboardAction.Hold;

            if (Input.GetKeyDown(KeyCode.Space)) action = KeyboardAction.Reset;
            else if (Input.GetKeyDown(KeyCode.R)) action = KeyboardAction.Rotate;
            else if (Input.GetKeyDown(KeyCode.S)) action = KeyboardAction.Spawn;
            else if (Input.GetKeyDown(KeyCode.M)) action = KeyboardAction.Move;
            else if (Input.GetKeyDown(KeyCode.D)) action = KeyboardAction.Delete;
            else if (Input.GetKeyDown(KeyCode.L)) action = KeyboardAction.ManageList;
            //else if (Input.GetKeyDown(KeyCode.L)) action = KeyboardAction.SelectSecondary;

            return action;
        }

        public static KeyboardSwitch GetKeyboardSwitch()
        {
            KeyboardSwitch action = KeyboardSwitch.Hold;

            if (Input.GetKeyDown(KeyCode.Tab)) action = KeyboardSwitch.ChangeMode;
         
            return action;
        }


         public static KeyboardRotations GetRotationInput()
        {
            KeyboardRotations action = 0;

            if (Input.GetKey(KeyCode.A))  action += (int)KeyboardRotations.RotateLeft;
            else if (Input.GetKey(KeyCode.D))  action += (int)KeyboardRotations.RotateRight;
            if (Input.GetKey(KeyCode.W))  action += (int)KeyboardRotations.RotateUp;
            else if (Input.GetKey(KeyCode.S))  action += (int)KeyboardRotations.RotateDown;

            return action;
        }


    }
}
