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
        public static KeyboardActions GetKeyboardAction()
        {
            KeyboardActions action = KeyboardActions.Hold;

            if (Input.GetKeyDown(KeyCode.Space)) action = KeyboardActions.Reset;
            else if (Input.GetKeyDown(KeyCode.R)) action = KeyboardActions.Rotate;
            else if (Input.GetKeyDown(KeyCode.S)) action = KeyboardActions.Spawn;
            else if (Input.GetKeyDown(KeyCode.M)) action = KeyboardActions.Move;

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
